import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/attendance.dart';
import '../../domain/repositories/attendance_repository.dart';
import '../../../players/domain/entities/player.dart';
import '../../../players/domain/repositories/player_repository.dart';

class AttendancePage extends StatefulWidget {
  final String role;
  final int userId;
  const AttendancePage({super.key, required this.role, required this.userId});

  @override
  State<AttendancePage> createState() => _AttendancePageState();
}

class _AttendancePageState extends State<AttendancePage> with SingleTickerProviderStateMixin {
  late TabController _tabController;
  List<Player> _players = [];
  List<Attendance> _history = [];
  bool _isLoading = true;
  final Map<int, String> _attendanceStatus = {};
  
  // Ciclo de estados solicitado
  final List<String> _statusCycle = ['Presente', 'Retardo', 'Falta', 'Justificado'];

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 2, vsync: this);
    _loadData();
  }

  Future<void> _loadData() async {
    if (widget.role == 'coach') {
      final playersResult = await sl<PlayerRepository>().getPlayers();
      playersResult.fold(
        (failure) => null,
        (players) {
          _players = players;
          for (var p in players) {
            // Inicializamos todos como 'Presente'
            _attendanceStatus[p.id] = _statusCycle[0];
          }
        },
      );
      
      List<Attendance> allHistory = [];
      for (var player in _players) {
        final result = await sl<AttendanceRepository>().getAttendanceByPlayer(player.id);
        result.fold((_) => null, (list) => allHistory.addAll(list));
      }
      allHistory.sort((a, b) => b.date.compareTo(a.date));
      _history = allHistory;
    } else {
      final playersResult = await sl<PlayerRepository>().getPlayersByTutor(widget.userId);
      playersResult.fold(
        (failure) => null,
        (players) async {
          _players = players;
          List<Attendance> allHistory = [];
          for (var player in players) {
            final result = await sl<AttendanceRepository>().getAttendanceByPlayer(player.id);
            result.fold((_) => null, (list) => allHistory.addAll(list));
          }
          allHistory.sort((a, b) => b.date.compareTo(a.date));
          _history = allHistory;
        },
      );
      _tabController.index = 1;
    }

    if (mounted) {
      setState(() => _isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Asistencias'),
        bottom: widget.role == 'coach' 
          ? TabBar(
              controller: _tabController,
              tabs: const [Tab(text: 'Registrar'), Tab(text: 'Historial')],
            )
          : null,
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : widget.role == 'coach'
              ? TabBarView(
                  controller: _tabController,
                  children: [_buildRegisterTab(), _buildHistoryTab()],
                )
              : _buildHistoryTab(),
    );
  }

  Widget _buildRegisterTab() {
    return Column(
      children: [
        Expanded(
          child: ListView.builder(
            padding: const EdgeInsets.all(16),
            itemCount: _players.length,
            itemBuilder: (context, index) {
              final player = _players[index];
              final currentStatus = _attendanceStatus[player.id] ?? _statusCycle[0];
              
              return Card(
                child: ListTile(
                  title: Text(player.name),
                  subtitle: Text(player.team),
                  trailing: SizedBox(
                    width: 120,
                    child: ElevatedButton(
                      onPressed: () => _cycleStatus(player.id),
                      style: ElevatedButton.styleFrom(
                        backgroundColor: _getStatusColor(currentStatus),
                        foregroundColor: Colors.white,
                        padding: const EdgeInsets.symmetric(horizontal: 4),
                        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
                      ),
                      child: Text(currentStatus, style: const TextStyle(fontSize: 12, fontWeight: FontWeight.bold)),
                    ),
                  ),
                ),
              );
            },
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(16.0),
          child: ElevatedButton(
            onPressed: _saveAttendance,
            style: ElevatedButton.styleFrom(
              backgroundColor: AppColors.primaryBlue,
              foregroundColor: Colors.white,
              minimumSize: const Size(double.infinity, 50),
            ),
            child: const Text('Guardar Asistencias del Día'),
          ),
        ),
      ],
    );
  }

  void _cycleStatus(int playerId) {
    setState(() {
      final currentStatus = _attendanceStatus[playerId] ?? _statusCycle[0];
      final currentIndex = _statusCycle.indexOf(currentStatus);
      final nextIndex = (currentIndex + 1) % _statusCycle.length;
      _attendanceStatus[playerId] = _statusCycle[nextIndex];
    });
  }

  Widget _buildHistoryTab() {
    return _history.isEmpty
        ? const Center(child: Text('No hay historial de asistencia'))
        : ListView.builder(
            padding: const EdgeInsets.all(16),
            itemCount: _history.length,
            itemBuilder: (context, index) {
              final attendance = _history[index];
              
              String playerName = 'Desconocido';
              try {
                final player = _players.firstWhere((p) => p.id == attendance.playerId);
                playerName = player.name;
              } catch (e) {}

              return ListTile(
                leading: CircleAvatar(
                  backgroundColor: _getStatusColor(attendance.status),
                  radius: 12,
                ),
                title: Text(playerName),
                subtitle: Text(attendance.date),
                trailing: Text(
                  attendance.status, 
                  style: TextStyle(
                    fontWeight: FontWeight.bold, 
                    color: _getStatusColor(attendance.status)
                  )
                ),
              );
            },
          );
  }

  Color _getStatusColor(String status) {
    switch (status) {
      case 'Presente': return Colors.green;
      case 'Retardo': return Colors.orange;
      case 'Falta': return Colors.red;
      case 'Justificado': return Colors.blue;
      default: return Colors.grey;
    }
  }

  void _saveAttendance() async {
    int savedCount = 0;
    final today = DateTime.now().toString().split(' ')[0];
    
    for (var entry in _attendanceStatus.entries) {
      final attendance = Attendance(id: 0, playerId: entry.key, date: today, status: entry.value);
      await sl<AttendanceRepository>().createAttendance(attendance);
      savedCount++;
    }

    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Se registraron $savedCount asistencias'), backgroundColor: Colors.green));
      _loadData();
      _tabController.animateTo(1);
    }
  }
}
