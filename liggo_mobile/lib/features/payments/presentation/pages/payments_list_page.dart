import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/payment.dart';
import '../../domain/repositories/payment_repository.dart';
import '../../../players/domain/repositories/player_repository.dart';

class PaymentsListPage extends StatefulWidget {
  final int userId;
  const PaymentsListPage({super.key, required this.userId});

  @override
  State<PaymentsListPage> createState() => _PaymentsListPageState();
}

class _PaymentsListPageState extends State<PaymentsListPage> {
  List<Payment> _payments = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadPayments();
  }

  Future<void> _loadPayments() async {
    final playersResult = await sl<PlayerRepository>().getPlayersByTutor(widget.userId);
    playersResult.fold(
      (failure) => setState(() => _isLoading = false),
      (players) async {
        List<Payment> allPayments = [];
        for (var player in players) {
          final result = await sl<PaymentRepository>().getPaymentsByPlayer(player.id);
          result.fold((_) => null, (list) => allPayments.addAll(list));
        }
        allPayments.sort((a, b) => b.date.compareTo(a.date));
        if (mounted) {
          setState(() {
            _payments = allPayments;
            _isLoading = false;
          });
        }
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Historial de Pagos')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _payments.isEmpty
              ? const Center(child: Text('No hay pagos registrados'))
              : ListView.builder(
                  padding: const EdgeInsets.all(16),
                  itemCount: _payments.length,
                  itemBuilder: (context, index) {
                    final payment = _payments[index];
                    return Card(
                      margin: const EdgeInsets.only(bottom: 12),
                      child: ListTile(
                        leading: const CircleAvatar(
                          backgroundColor: AppColors.primaryBlue,
                          child: Icon(Icons.attach_money, color: Colors.white),
                        ),
                        title: Text('Mes: ${payment.month}', style: const TextStyle(fontWeight: FontWeight.bold)),
                        subtitle: Text('Fecha: ${payment.date}\nMonto: \$${payment.amount.toStringAsFixed(2)}'),
                        trailing: Container(
                          padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                          decoration: BoxDecoration(
                            color: _getStatusColor(payment.status).withAlpha(30),
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: Text(
                            payment.status.toUpperCase(),
                            style: TextStyle(color: _getStatusColor(payment.status), fontWeight: FontWeight.bold, fontSize: 12),
                          ),
                        ),
                        isThreeLine: true,
                      ),
                    );
                  },
                ),
    );
  }

  Color _getStatusColor(String status) {
    switch (status.toLowerCase()) {
      case 'pagado': return Colors.green;
      case 'pendiente': return Colors.orange;
      case 'vencido': return Colors.red;
      default: return Colors.grey;
    }
  }
}
