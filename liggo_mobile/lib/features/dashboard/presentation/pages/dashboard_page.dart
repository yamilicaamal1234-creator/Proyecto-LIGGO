import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../auth/domain/entities/user.dart';
import '../../../auth/presentation/pages/login_page.dart';
import '../../../reports/presentation/pages/create_report_page.dart';
import '../../../reports/presentation/pages/reports_list_page.dart';
import '../../../incidents/presentation/pages/create_incident_page.dart';
import '../../../incidents/presentation/pages/incidents_list_page.dart';
import '../../../attendance/presentation/pages/attendance_page.dart';
import '../../../matches/presentation/pages/matches_list_page.dart';
import '../../../payments/presentation/pages/payments_list_page.dart';

class DashboardPage extends StatelessWidget {
  final User user;
  const DashboardPage({super.key, required this.user});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Dashboard LIGGO'),
        backgroundColor: AppColors.primaryBlue,
        foregroundColor: Colors.white,
        actions: [
          IconButton(
            icon: const Icon(Icons.logout),
            onPressed: () {
              Navigator.pushAndRemoveUntil(
                context,
                MaterialPageRoute(builder: (context) => const LoginPage()),
                (route) => false,
              );
            },
          ),
        ],
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            _buildWelcomeCard(),
            const SizedBox(height: 24),
            const Text(
              'Opciones del sistema',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 16),
            if (user.rol == 'coach') ..._buildCoachOptions(context),
            if (user.rol == 'tutor') ..._buildTutorOptions(context),
          ],
        ),
      ),
    );
  }

  Widget _buildWelcomeCard() {
    return Card(
      color: AppColors.secondaryBlue,
      child: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          children: [
            const CircleAvatar(
              radius: 40,
              backgroundColor: Colors.white,
              child: Icon(Icons.person, size: 50, color: AppColors.primaryBlue),
            ),
            const SizedBox(height: 16),
            Text(
              '¡Bienvenido, ${user.nombre}!',
              style: const TextStyle(fontSize: 22, fontWeight: FontWeight.bold, color: Colors.white),
            ),
            Text(
              'Rol: ${user.rol.toUpperCase()}',
              style: const TextStyle(fontSize: 16, color: Colors.white70),
            ),
            const SizedBox(height: 8),
            Container(
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 4),
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.circular(20),
              ),
              child: const Text(
                'Suscripción activa',
                style: TextStyle(color: AppColors.primaryBlue, fontWeight: FontWeight.bold),
              ),
            ),
          ],
        ),
      ),
    );
  }

  List<Widget> _buildCoachOptions(BuildContext context) {
    return [
      _buildOptionCard(context, Icons.add_chart, 'Crear reporte', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => CreateReportPage(coachId: user.id)));
      }),
      _buildOptionCard(context, Icons.list_alt, 'Ver reportes creados', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => ReportsListPage(role: 'coach', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.warning_amber_rounded, 'Registrar Incidencia', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => CreateIncidentPage(coachId: user.id)));
      }),
      _buildOptionCard(context, Icons.history, 'Ver Incidencias', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => IncidentsListPage(role: 'coach', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.how_to_reg, 'Pasar Asistencia', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => AttendancePage(role: 'coach', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.person, 'Perfil', () {}),
    ];
  }

  List<Widget> _buildTutorOptions(BuildContext context) {
    return [
      _buildOptionCard(context, Icons.sports_soccer, 'Próximos Partidos', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => MatchesListPage(userId: user.id)));
      }),
      _buildOptionCard(context, Icons.history_edu, 'Ver reportes del coach', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => ReportsListPage(role: 'tutor', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.check_circle_outline, 'Ver Asistencias', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => AttendancePage(role: 'tutor', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.warning_amber, 'Ver Incidencias', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => IncidentsListPage(role: 'tutor', userId: user.id)));
      }),
      _buildOptionCard(context, Icons.monetization_on_outlined, 'Historial de Pagos', () {
        Navigator.push(context, MaterialPageRoute(builder: (context) => PaymentsListPage(userId: user.id)));
      }),
      _buildOptionCard(context, Icons.person, 'Perfil', () {}),
    ];
  }

  Widget _buildOptionCard(BuildContext context, IconData icon, String title, VoidCallback onTap) {
    return Card(
      margin: const EdgeInsets.only(bottom: 16),
      child: ListTile(
        leading: Icon(icon, color: AppColors.primaryBlue, size: 30),
        title: Text(title, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.w500)),
        trailing: const Icon(Icons.chevron_right),
        onTap: onTap,
      ),
    );
  }
}
