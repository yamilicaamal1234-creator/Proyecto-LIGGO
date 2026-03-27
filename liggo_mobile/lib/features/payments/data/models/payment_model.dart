import '../../domain/entities/payment.dart';

class PaymentModel extends Payment {
  const PaymentModel({
    required super.id,
    required super.month,
    required super.amount,
    required super.status,
    required super.date,
    required super.playerId,
  });

  factory PaymentModel.fromJson(Map<String, dynamic> json) {
    return PaymentModel(
      id: json['id'],
      month: json['month'],
      amount: (json['amount'] as num).toDouble(),
      status: json['status'],
      date: json['date'],
      playerId: json['playerId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'month': month,
      'amount': amount,
      'status': status,
      'date': date,
      'playerId': playerId,
    };
  }
}
