import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/player.dart';
import '../../domain/repositories/player_repository.dart';
import '../datasources/player_local_data_source.dart';

class PlayerRepositoryImpl implements PlayerRepository {
  final PlayerLocalDataSource localDataSource;

  PlayerRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, List<Player>>> getPlayers() async {
    try {
      final players = await localDataSource.getPlayers();
      return Right(players);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Player>>> getPlayersByTutor(int tutorId) async {
    try {
      final players = await localDataSource.getPlayers();
      final filtered = players.where((p) => p.tutorId == tutorId).toList();
      return Right(filtered);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
