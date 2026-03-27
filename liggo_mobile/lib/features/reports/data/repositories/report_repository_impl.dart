import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/report.dart';
import '../../domain/repositories/report_repository.dart';
import '../datasources/report_local_data_source.dart';
import '../models/report_model.dart';

class ReportRepositoryImpl implements ReportRepository {
  final ReportLocalDataSource localDataSource;

  ReportRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, Report>> createReport(Report report) async {
    try {
      final reportModel = ReportModel(
        reportId: report.reportId,
        coachId: report.coachId,
        playerId: report.playerId,
        playerName: report.playerName,
        title: report.title,
        date: report.date,
        performance: report.performance,
        comments: report.comments,
      );
      await localDataSource.saveReport(reportModel);
      return Right(reportModel);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Report>>> getReportsByCoach(int coachId) async {
    try {
      final reports = await localDataSource.getReports();
      return Right(reports.where((r) => r.coachId == coachId).toList());
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Report>>> getReportsByPlayer(String playerName) async {
    try {
      final reports = await localDataSource.getReports();
      return Right(reports.where((r) => r.playerName == playerName).toList());
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Report>>> getReportsByPlayerId(int playerId) async {
    try {
      final reports = await localDataSource.getReports();
      return Right(reports.where((r) => r.playerId == playerId).toList());
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
