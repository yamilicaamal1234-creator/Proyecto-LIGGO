import 'package:equatable/equatable.dart';

class Match extends Equatable {
  final int id;
  final String rival;
  final String date;
  final String result;
  final int playerId;

  const Match({
    required this.id,
    required this.rival,
    required this.date,
    required this.result,
    required this.playerId,
  });

  @override
  List<Object?> get props => [id, rival, date, result, playerId];
}
