import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../subscription/presentation/pages/payment_page.dart';

class SubscriptionPlansPage extends StatelessWidget {
  final int userId;
  const SubscriptionPlansPage({super.key, required this.userId});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Selecciona tu plan')),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          children: [
            _buildPlanCard(
              context,
              title: 'Plan Individual',
              price: '\$600 MXN al mes',
              features: ['Entrenadores', 'Academias pequeñas', 'Reportes básicos'],
              color: AppColors.secondaryBlue,
              onTap: () => Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => PaymentPage(userId: userId, plan: 'Individual'),
                ),
              ),
            ),
            const SizedBox(height: 24),
            _buildPlanCard(
              context,
              title: 'Plan Academia',
              price: '\$1700 MXN al mes',
              features: ['Organizaciones grandes', 'Control avanzado', 'Reportes detallados'],
              color: AppColors.primaryBlue,
              onTap: () => Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => PaymentPage(userId: userId, plan: 'Academia'),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildPlanCard(BuildContext context,
      {required String title,
      required String price,
      required List<String> features,
      required Color color,
      required VoidCallback onTap}) {
    return Card(
      elevation: 4,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(16),
        child: Padding(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Text(title, style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold, color: color)),
              const SizedBox(height: 8),
              Text(price, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.w500)),
              const SizedBox(height: 16),
              ...features.map((f) => Padding(
                    padding: const EdgeInsets.only(bottom: 4),
                    child: Row(children: [const Icon(Icons.check, size: 18), const SizedBox(width: 8), Text(f)]),
                  )),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: onTap,
                style: ElevatedButton.styleFrom(backgroundColor: color, foregroundColor: Colors.white),
                child: const Text('Seleccionar'),
              ),
            ],
          ),
        ),
      ),
    );
  }

}

