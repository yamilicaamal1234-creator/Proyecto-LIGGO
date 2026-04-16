import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/incident.dart';
import '../../domain/repositories/incident_repository.dart';
import '../../../players/domain/repositories/player_repository.dart';
import '../../../players/domain/entities/player.dart';

class IncidentsListPage extends StatefulWidget {
  final String role;
  final int userId;
  const IncidentsListPage({super.key, required this.role, required this.userId});

  @override
  State<IncidentsListPage> createState() => _IncidentsListPageState();
}

class _IncidentsListPageState extends State<IncidentsListPage> {
  List<Incident> _incidents = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadIncidents();
  }

  Future<void> _loadIncidents() async {
    if (widget.role == 'coach') {
      // For coach, we show all incidents from all players
      // For simplicity, we just get all incidents. 
      // In a real app we might filter by coach's team.
      final playersResult = await sl<PlayerRepository>().getPlayers();
      playersResult.fold(
        (failure) => setState(() => _isLoading = false),
        (players) async {
          List<Incident> allIncidents = [];
          for (var player in players) {
            final result = await sl<IncidentRepository>().getIncidentsByPlayer(player.id);
            result.fold((_) => null, (list) => allIncidents.addAll(list));
          }
          allIncidents.sort((a, b) => b.date.compareTo(a.date));
          if (mounted) {
            setState(() {
              _incidents = allIncidents;
              _isLoading = false;
            });
          }
        },
      );
    } else {
      final playersResult = await sl<PlayerRepository>().getPlayersByTutor(widget.userId);
      playersResult.fold(
        (failure) => setState(() => _isLoading = false),
        (players) async {
          List<Incident> allIncidents = [];
          for (var player in players) {
            final result = await sl<IncidentRepository>().getIncidentsByPlayer(player.id);
            result.fold((_) => null, (list) => allIncidents.addAll(list));
          }
          allIncidents.sort((a, b) => b.date.compareTo(a.date));
          if (mounted) {
            setState(() {
              _incidents = allIncidents;
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
      appBar: AppBar(title: const Text('Incidencias')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _incidents.isEmpty
              ? const Center(child: Text('No hay incidencias registradas'))
              : ListView.builder(
                  padding: const EdgeInsets.all(16),
                  itemCount: _incidents.length,
                  itemBuilder: (context, index) {
                    final incident = _incidents[index];
                    return Card(
                      margin: const EdgeInsets.only(bottom: 12),
                      child: ListTile(
                        leading: CircleAvatar(
                          backgroundColor: _getIncidentColor(incident.type),
                          child: const Icon(Icons.warning_amber, color: Colors.white),
                        ),
                        title: Text(incident.type, style: const TextStyle(fontWeight: FontWeight.bold)),
                        subtitle: Text('${incident.date}\n${incident.description}'),
                        isThreeLine: true,
                      ),
                    );
                  },
                ),
    );
  }

  Color _getIncidentColor(String type) {
    switch (type.toLowerCase()) {
      case 'lesión': return Colors.orange;
      case 'disciplina': return Colors.red;
      case 'otros': return Colors.blue;
      default: return Colors.grey;
    }
  }
}
