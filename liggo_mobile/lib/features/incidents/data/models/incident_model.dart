import '../../domain/entities/incident.dart';

class IncidentModel extends Incident {
  const IncidentModel({
    required super.id,
    required super.type,
    required super.description,
    required super.date,
    required super.playerId,
  });

  factory IncidentModel.fromJson(Map<String, dynamic> json) {
    return IncidentModel(
      id: json['id'],
      type: json['type'],
      description: json['description'],
      date: json['date'],
      playerId: json['playerId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'type': type,
      'description': description,
      'date': date,
      'playerId': playerId,
    };
  }
}
