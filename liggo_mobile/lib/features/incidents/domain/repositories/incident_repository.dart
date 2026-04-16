import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../entities/incident.dart';

abstract class IncidentRepository {
  Future<Either<Failure, Incident>> createIncident(Incident incident);
  Future<Either<Failure, List<Incident>>> getIncidentsByPlayer(int playerId);
}
