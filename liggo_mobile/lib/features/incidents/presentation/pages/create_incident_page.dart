import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/incident.dart';
import '../../domain/repositories/incident_repository.dart';
import '../../../players/domain/entities/player.dart';
import '../../../players/domain/repositories/player_repository.dart';

class CreateIncidentPage extends StatefulWidget {
  final int coachId;
  const CreateIncidentPage({super.key, required this.coachId});

  @override
  State<CreateIncidentPage> createState() => _CreateIncidentPageState();
}

class _CreateIncidentPageState extends State<CreateIncidentPage> {
  final _descriptionController = TextEditingController();
  String _type = 'Lesión';
  Player? _selectedPlayer;
  List<Player> _players = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadPlayers();
  }

  Future<void> _loadPlayers() async {
    final result = await sl<PlayerRepository>().getPlayers();
    result.fold(
      (failure) => null,
      (players) => setState(() {
        _players = players;
        _isLoading = false;
      }),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Registrar Incidencia')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : SingleChildScrollView(
              padding: const EdgeInsets.all(24.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  const Text('Seleccionar Jugador:', style: TextStyle(fontWeight: FontWeight.bold)),
                  DropdownButton<Player>(
                    value: _selectedPlayer,
                    isExpanded: true,
                    hint: const Text('Seleccione un jugador'),
                    items: _players
                        .map((p) => DropdownMenuItem(value: p, child: Text(p.name)))
                        .toList(),
                    onChanged: (v) => setState(() => _selectedPlayer = v),
                  ),
                  const SizedBox(height: 16),
                  const Text('Tipo de Incidencia:', style: TextStyle(fontWeight: FontWeight.bold)),
                  DropdownButton<String>(
                    value: _type,
                    isExpanded: true,
                    items: ['Lesión', 'Disciplina', 'Médica', 'Otros']
                        .map((e) => DropdownMenuItem(value: e, child: Text(e)))
                        .toList(),
                    onChanged: (v) => setState(() => _type = v!),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    controller: _descriptionController,
                    maxLines: 4,
                    decoration: const InputDecoration(labelText: 'Descripción detallada', border: OutlineInputBorder()),
                  ),
                  const SizedBox(height: 24),
                  ElevatedButton(
                    onPressed: _selectedPlayer == null ? null : _saveIncident,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.orange,
                      foregroundColor: Colors.white,
                      padding: const EdgeInsets.symmetric(vertical: 16),
                    ),
                    child: const Text('Registrar Incidencia', style: TextStyle(fontSize: 18)),
                  ),
                ],
              ),
            ),
    );
  }

  void _saveIncident() async {
    if (_selectedPlayer == null) return;

    final incident = Incident(
      id: 0,
      type: _type,
      description: _descriptionController.text,
      date: DateTime.now().toString().split(' ')[0],
      playerId: _selectedPlayer!.id,
    );
    
    final result = await sl<IncidentRepository>().createIncident(incident);
    
    if (mounted) {
      result.fold(
        (failure) => ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(failure.message))),
        (success) {
          ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('Incidencia registrada con éxito'), backgroundColor: Colors.orange));
          Navigator.pop(context);
        },
      );
    }
  }
}
