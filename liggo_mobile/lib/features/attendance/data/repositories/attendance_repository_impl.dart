import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/attendance.dart';
import '../../domain/repositories/attendance_repository.dart';
import '../datasources/attendance_local_data_source.dart';
import '../models/attendance_model.dart';

class AttendanceRepositoryImpl implements AttendanceRepository {
  final AttendanceLocalDataSource localDataSource;

  AttendanceRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, Attendance>> createAttendance(Attendance attendance) async {
    try {
      final model = AttendanceModel(
        id: attendance.id,
        playerId: attendance.playerId,
        date: attendance.date,
        status: attendance.status,
      );
      await localDataSource.saveAttendance(model);
      return Right(model);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, List<Attendance>>> getAttendanceByPlayer(int playerId) async {
    try {
      final list = await localDataSource.getAttendance();
      final filtered = list.where((a) => a.playerId == playerId).toList();
      return Right(filtered);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }
}
