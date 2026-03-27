import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/match.dart' as entity;
import '../../domain/repositories/match_repository.dart';
import '../datasources/match_local_data_source.dart';

class MatchRepositoryImpl implements MatchRepository {
  final MatchLocalDataSource localDataSource;

  MatchRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, List<entity.Match>>> getMatchesByPlayer(int playerId) async {
    try {
      final matches = await localDataSource.getMatches();
      final filtered = matches.where((m) => m.playerId == playerId).toList();
      return Right(filtered);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
