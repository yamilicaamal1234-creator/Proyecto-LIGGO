import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../entities/player.dart';

abstract class PlayerRepository {
  Future<Either<Failure, List<Player>>> getPlayers();
  Future<Either<Failure, List<Player>>> getPlayersByTutor(int tutorId);
}
