import 'package:equatable/equatable.dart';

class Player extends Equatable {
  final int id;
  final String name;
  final String team;
  final int tutorId;

  const Player({
    required this.id,
    required this.name,
    required this.team,
    required this.tutorId,
  });

  @override
  List<Object?> get props => [id, name, team, tutorId];
}
