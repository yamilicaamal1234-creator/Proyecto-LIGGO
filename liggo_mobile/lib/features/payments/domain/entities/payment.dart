import 'package:equatable/equatable.dart';

class Payment extends Equatable {
  final int id;
  final String month;
  final double amount;
  final String status;
  final String date;
  final int playerId;

  const Payment({
    required this.id,
    required this.month,
    required this.amount,
    required this.status,
    required this.date,
    required this.playerId,
  });

  @override
  List<Object?> get props => [id, month, amount, status, date, playerId];
}
