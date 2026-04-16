import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../entities/subscription.dart';

abstract class SubscriptionRepository {
  Future<Either<Failure, Subscription>> selectPlan(int userId, String plan);
  Future<Either<Failure, bool>> completePayment(int userId);
  Future<Either<Failure, Subscription?>> getSubscription(int userId);
}
