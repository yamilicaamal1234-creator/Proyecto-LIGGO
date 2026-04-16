import '../../domain/entities/match.dart';

class MatchModel extends Match {
  const MatchModel({
    required super.id,
    required super.rival,
    required super.date,
    required super.result,
    required super.playerId,
  });

  factory MatchModel.fromJson(Map<String, dynamic> json) {
    return MatchModel(
      id: json['id'],
      rival: json['rival'],
      date: json['date'],
      result: json['result'],
      playerId: json['playerId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'rival': rival,
      'date': date,
      'result': result,
      'playerId': playerId,
    };
  }
}
