import 'package:equatable/equatable.dart';

class Attendance extends Equatable {
  final int id;
  final int playerId;
  final String date;
  final String status;

  const Attendance({
    required this.id,
    required this.playerId,
    required this.date,
    required this.status,
  });

  @override
  List<Object?> get props => [id, playerId, date, status];
}
