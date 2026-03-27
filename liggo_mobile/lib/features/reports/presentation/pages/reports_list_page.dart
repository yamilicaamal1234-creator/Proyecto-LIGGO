import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/report.dart';
import '../../domain/repositories/report_repository.dart';
import '../../../players/domain/repositories/player_repository.dart';

class ReportsListPage extends StatefulWidget {
  final String role;
  final int userId;
  const ReportsListPage({super.key, required this.role, required this.userId});

  @override
  State<ReportsListPage> createState() => _ReportsListPageState();
}

class _ReportsListPageState extends State<ReportsListPage> {
  List<Report> _reports = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadReports();
  }

  Future<void> _loadReports() async {
    if (widget.role == 'coach') {
      final result = await sl<ReportRepository>().getReportsByCoach(widget.userId);
      result.fold(
        (failure) => setState(() => _isLoading = false),
        (reports) => setState(() {
          _reports = reports;
          _isLoading = false;
        }),
      );
    } else {
      // Role is tutor
      final playersResult = await sl<PlayerRepository>().getPlayersByTutor(widget.userId);
      playersResult.fold(
        (failure) => setState(() => _isLoading = false),
        (players) async {
          List<Report> allReports = [];
          for (var player in players) {
            final reportsResult = await sl<ReportRepository>().getReportsByPlayerId(player.id);
            reportsResult.fold((_) => null, (reports) => allReports.addAll(reports));
          }
          // Sort by date descending
          allReports.sort((a, b) => b.date.compareTo(a.date));
          if (mounted) {
            setState(() {
              _reports = allReports;
              _isLoading = false;
            });
          }
        },
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Reportes de Entrenamiento')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _reports.isEmpty
              ? const Center(child: Text('No hay reportes disponibles'))
              : ListView.builder(
                  padding: const EdgeInsets.all(16),
                  itemCount: _reports.length,
                  itemBuilder: (context, index) {
                    final report = _reports[index];
                    return Card(
                      margin: const EdgeInsets.only(bottom: 12),
                      child: ListTile(
                        title: Text(report.title, style: const TextStyle(fontWeight: FontWeight.bold)),
                        subtitle: Text('${report.playerName} - ${report.date}\nDesempeño: ${report.performance}'),
                        trailing: const Icon(Icons.description, color: AppColors.primaryBlue),
                        isThreeLine: true,
                        onTap: () => _showReportDetails(report),
                      ),
                    );
                  },
                ),
    );
  }

  void _showReportDetails(Report report) {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      builder: (context) => Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(report.title, style: const TextStyle(fontSize: 20, fontWeight: FontWeight.bold, color: AppColors.primaryBlue)),
            const Divider(),
            Text('Jugador: ${report.playerName}', style: const TextStyle(fontSize: 16)),
            Text('Fecha: ${report.date}'),
            Text('Desempeño: ${report.performance}', style: const TextStyle(fontWeight: FontWeight.bold)),
            const SizedBox(height: 16),
            const Text('Observaciones:', style: TextStyle(fontWeight: FontWeight.bold)),
            Text(report.comments),
            const SizedBox(height: 24),
            ElevatedButton(
              onPressed: () => Navigator.pop(context),
              style: ElevatedButton.styleFrom(backgroundColor: AppColors.primaryBlue, foregroundColor: Colors.white),
              child: const Text('Cerrar'),
            ),
          ],
        ),
      ),
    );
  }
}
