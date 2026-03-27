import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../../auth/data/datasources/auth_local_data_source.dart';
import '../../domain/entities/subscription.dart';
import '../../domain/repositories/subscription_repository.dart';
import '../datasources/subscription_local_data_source.dart';
import '../models/subscription_model.dart';

class SubscriptionRepositoryImpl implements SubscriptionRepository {
  final SubscriptionLocalDataSource localDataSource;
  final AuthLocalDataSource authLocalDataSource;

  SubscriptionRepositoryImpl({
    required this.localDataSource,
    required this.authLocalDataSource,
  });

  @override
  Future<Either<Failure, Subscription>> selectPlan(int userId, String plan) async {
    try {
      final subscription = SubscriptionModel(userId: userId, plan: plan, status: 'pendiente');
      await localDataSource.saveSubscription(subscription);
      return Right(subscription);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, bool>> completePayment(int userId) async {
    try {
      await localDataSource.updateSubscriptionStatus(userId, 'activo');
      await authLocalDataSource.updateUserPlan(userId, true);
      return const Right(true);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, Subscription?>> getSubscription(int userId) async {
    try {
      final result = await localDataSource.getSubscription(userId);
      return Right(result);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
