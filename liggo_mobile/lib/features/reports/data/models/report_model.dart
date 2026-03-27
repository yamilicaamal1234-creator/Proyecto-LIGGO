import '../../domain/entities/report.dart';

class ReportModel extends Report {
  const ReportModel({
    required super.reportId,
    required super.coachId,
    super.playerId,
    required super.playerName,
    required super.title,
    required super.date,
    required super.performance,
    required super.comments,
  });

  factory ReportModel.fromJson(Map<String, dynamic> json) {
    return ReportModel(
      reportId: json['reportId'],
      coachId: json['coachId'],
      playerId: json['playerId'],
      playerName: json['playerName'],
      title: json['title'] ?? 'Reporte de entrenamiento',
      date: json['date'],
      performance: json['performance'],
      comments: json['comments'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'reportId': reportId,
      'coachId': coachId,
      'playerId': playerId,
      'playerName': playerName,
      'title': title,
      'date': date,
      'performance': performance,
      'comments': comments,
    };
  }
}
