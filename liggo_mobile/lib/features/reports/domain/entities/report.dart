import 'package:equatable/equatable.dart';

class Report extends Equatable {
  final int reportId;
  final int coachId;
  final int? playerId;
  final String playerName;
  final String title;
  final String date;
  final String performance;
  final String comments;

  const Report({
    required this.reportId,
    required this.coachId,
    this.playerId,
    required this.playerName,
    required this.title,
    required this.date,
    required this.performance,
    required this.comments,
  });

  @override
  List<Object?> get props => [reportId, coachId, playerId, playerName, title, date, performance, comments];
}
