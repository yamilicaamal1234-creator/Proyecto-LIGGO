import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/incident.dart';
import '../../domain/repositories/incident_repository.dart';
import '../datasources/incident_local_data_source.dart';
import '../models/incident_model.dart';

class IncidentRepositoryImpl implements IncidentRepository {
  final IncidentLocalDataSource localDataSource;

  IncidentRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, Incident>> createIncident(Incident incident) async {
    try {
      final incidentModel = IncidentModel(
        id: incident.id,
        type: incident.type,
        description: incident.description,
        date: incident.date,
        playerId: incident.playerId,
      );
      await localDataSource.saveIncident(incidentModel);
      return Right(incidentModel);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Incident>>> getIncidentsByPlayer(int playerId) async {
    try {
      final incidents = await localDataSource.getIncidents();
      final filtered = incidents.where((i) => i.playerId == playerId).toList();
      return Right(filtered);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
