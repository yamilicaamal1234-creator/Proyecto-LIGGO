import 'package:equatable/equatable.dart';

class Incident extends Equatable {
  final int id;
  final String type;
  final String description;
  final String date;
  final int playerId;

  const Incident({
    required this.id,
    required this.type,
    required this.description,
    required this.date,
    required this.playerId,
  });

  @override
  List<Object?> get props => [id, type, description, date, playerId];
}
