import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../entities/report.dart';

abstract class ReportRepository {
  Future<Either<Failure, Report>> createReport(Report report);
  Future<Either<Failure, List<Report>>> getReportsByCoach(int coachId);
  Future<Either<Failure, List<Report>>> getReportsByPlayer(String playerName);
  Future<Either<Failure, List<Report>>> getReportsByPlayerId(int playerId);
}
