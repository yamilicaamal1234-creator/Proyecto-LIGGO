import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/entities/match.dart';
import '../../domain/repositories/match_repository.dart';
import '../../../players/domain/repositories/player_repository.dart';

class MatchesListPage extends StatefulWidget {
  final int userId;
  const MatchesListPage({super.key, required this.userId});

  @override
  State<MatchesListPage> createState() => _MatchesListPageState();
}

class _MatchesListPageState extends State<MatchesListPage> {
  List<Match> _matches = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadMatches();
  }

  Future<void> _loadMatches() async {
    final playersResult = await sl<PlayerRepository>().getPlayersByTutor(widget.userId);
    playersResult.fold(
      (failure) => setState(() => _isLoading = false),
      (players) async {
        List<Match> allMatches = [];
        for (var player in players) {
          final result = await sl<MatchRepository>().getMatchesByPlayer(player.id);
          result.fold((_) => null, (list) => allMatches.addAll(list));
        }
        allMatches.sort((a, b) => b.date.compareTo(a.date));
        if (mounted) {
          setState(() {
            _matches = allMatches;
            _isLoading = false;
          });
        }
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Próximos Partidos')),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _matches.isEmpty
              ? const Center(child: Text('No hay partidos programados'))
              : ListView.builder(
                  padding: const EdgeInsets.all(16),
                  itemCount: _matches.length,
                  itemBuilder: (context, index) {
                    final match = _matches[index];
                    return Card(
                      margin: const EdgeInsets.only(bottom: 12),
                      child: ListTile(
                        leading: CircleAvatar(
                          backgroundColor: _getResultColor(match.result),
                          child: const Icon(Icons.sports_soccer, color: Colors.white),
                        ),
                        title: Text('Vs ${match.rival}', style: const TextStyle(fontWeight: FontWeight.bold)),
                        subtitle: Text('Fecha: ${match.date}'),
                        trailing: Container(
                          padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                          decoration: BoxDecoration(
                            color: _getResultColor(match.result).withAlpha(30),
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: Text(
                            match.result.toUpperCase(),
                            style: TextStyle(color: _getResultColor(match.result), fontWeight: FontWeight.bold, fontSize: 12),
                          ),
                        ),
                      ),
                    );
                  },
                ),
    );
  }

  Color _getResultColor(String result) {
    switch (result.toLowerCase()) {
      case 'ganado': return Colors.green;
      case 'perdido': return Colors.red;
      case 'pendiente': return Colors.orange;
      default: return Colors.grey;
    }
  }
}
