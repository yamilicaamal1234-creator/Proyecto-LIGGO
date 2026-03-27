import '../../../../core/utils/json_helper.dart';
import '../models/attendance_model.dart';

abstract class AttendanceLocalDataSource {
  Future<List<AttendanceModel>> getAttendance();
  Future<void> saveAttendance(AttendanceModel attendance);
}

class AttendanceLocalDataSourceImpl implements AttendanceLocalDataSource {
  @override
  Future<List<AttendanceModel>> getAttendance() async {
    try {
      final data = await JsonHelper.readJson('attendance.json');
      if (data.isEmpty || data['attendance'] == null) return [];
      return (data['attendance'] as List)
          .map((a) => AttendanceModel.fromJson(a))
          .toList();
    } catch (e) {
      return [];
    }
  }

  @override
  Future<void> saveAttendance(AttendanceModel attendance) async {
    final history = await getAttendance();
    final newAttendance = AttendanceModel(
      id: history.isEmpty ? 1 : history.last.id + 1,
      playerId: attendance.playerId,
      date: attendance.date,
      status: attendance.status,
    );
    history.add(newAttendance);
    await JsonHelper.writeJson('attendance.json', {'attendance': history.map((a) => a.toJson()).toList()});
  }
}
