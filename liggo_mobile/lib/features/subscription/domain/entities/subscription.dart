import 'package:equatable/equatable.dart';

class Subscription extends Equatable {
  final int userId;
  final String plan;
  final String status;

  const Subscription({
    required this.userId,
    required this.plan,
    required this.status,
  });

  @override
  List<Object?> get props => [userId, plan, status];
}
