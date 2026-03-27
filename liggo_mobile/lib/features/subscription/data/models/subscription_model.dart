import '../../domain/entities/subscription.dart';

class SubscriptionModel extends Subscription {
  const SubscriptionModel({
    required super.userId,
    required super.plan,
    required super.status,
  });

  factory SubscriptionModel.fromJson(Map<String, dynamic> json) {
    return SubscriptionModel(
      userId: json['userId'],
      plan: json['plan'],
      status: json['status'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'userId': userId,
      'plan': plan,
      'status': status,
    };
  }
}
