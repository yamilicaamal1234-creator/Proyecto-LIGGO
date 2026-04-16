import '../../domain/entities/player.dart';

class PlayerModel extends Player {
  const PlayerModel({
    required super.id,
    required super.name,
    required super.team,
    required super.tutorId,
  });

  factory PlayerModel.fromJson(Map<String, dynamic> json) {
    return PlayerModel(
      id: json['id'],
      name: json['name'],
      team: json['team'],
      tutorId: json['tutorId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'team': team,
      'tutorId': tutorId,
    };
  }
}
