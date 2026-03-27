import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../entities/attendance.dart';

abstract class AttendanceRepository {
  Future<Either<Failure, Attendance>> createAttendance(Attendance attendance);
  Future<Either<Failure, List<Attendance>>> getAttendanceByPlayer(int playerId);
}
