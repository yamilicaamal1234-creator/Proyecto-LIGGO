import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/payment.dart';
import '../../domain/repositories/payment_repository.dart';
import '../datasources/payment_local_data_source.dart';

class PaymentRepositoryImpl implements PaymentRepository {
  final PaymentLocalDataSource localDataSource;

  PaymentRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, List<Payment>>> getPaymentsByPlayer(int playerId) async {
    try {
      final list = await localDataSource.getPayments();
      final filtered = list.where((p) => p.playerId == playerId).toList();
      return Right(filtered);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
