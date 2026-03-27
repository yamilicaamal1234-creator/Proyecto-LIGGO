import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/report.dart';
import '../../domain/repositories/report_repository.dart';
import '../../../players/domain/entities/player.dart';
import '../../../players/domain/repositories/player_repository.dart';

class CreateReportPage extends StatefulWidget {
  final int coachId;
  const CreateReportPage({super.key, required this.coachId});

  @override
  State<CreateReportPage> createState() => _CreateReportPageState();
}

class _CreateReportPageState extends State<CreateReportPage> {
  final _titleController = TextEditingController();
  final _commentsController = TextEditingController();
  String _performance = 'Bueno';
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
      appBar: AppBar(title: const Text('Crear Reporte')),
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
                  TextField(
                    controller: _titleController,
                    decoration: const InputDecoration(labelText: 'Título del reporte', border: OutlineInputBorder()),
                  ),
                  const SizedBox(height: 16),
                  const Text('Nivel de desempeño:', style: TextStyle(fontWeight: FontWeight.bold)),
                  DropdownButton<String>(
                    value: _performance,
                    isExpanded: true,
                    items: ['Excelente', 'Bueno', 'Regular', 'Bajo']
                        .map((e) => DropdownMenuItem(value: e, child: Text(e)))
                        .toList(),
                    onChanged: (v) => setState(() => _performance = v!),
                  ),
                  const SizedBox(height: 16),
                  TextField(
                    controller: _commentsController,
                    maxLines: 4,
                    decoration: const InputDecoration(labelText: 'Observaciones del entrenamiento', border: OutlineInputBorder()),
                  ),
                  const SizedBox(height: 24),
                  ElevatedButton(
                    onPressed: _selectedPlayer == null ? null : _saveReport,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: AppColors.primaryBlue,
                      foregroundColor: Colors.white,
                      padding: const EdgeInsets.symmetric(vertical: 16),
                    ),
                    child: const Text('Guardar Reporte', style: TextStyle(fontSize: 18)),
                  ),
                ],
              ),
            ),
    );
  }

  void _saveReport() async {
    if (_selectedPlayer == null) return;

    final report = Report(
      reportId: 0,
      coachId: widget.coachId,
      playerId: _selectedPlayer!.id,
      playerName: _selectedPlayer!.name,
      title: _titleController.text.isEmpty ? 'Reporte de entrenamiento' : _titleController.text,
      date: DateTime.now().toString().split(' ')[0],
      performance: _performance,
      comments: _commentsController.text,
    );
    
    final result = await sl<ReportRepository>().createReport(report);
    
    if (mounted) {
      result.fold(
        (failure) => ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(failure.message))),
        (success) {
          ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('Reporte guardado con éxito'), backgroundColor: Colors.green));
          Navigator.pop(context);
        },
      );
    }
  }
}
