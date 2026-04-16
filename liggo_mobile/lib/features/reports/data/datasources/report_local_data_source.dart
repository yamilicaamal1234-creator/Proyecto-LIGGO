import '../../../../core/utils/json_helper.dart';
import '../models/report_model.dart';

abstract class ReportLocalDataSource {
  Future<List<ReportModel>> getReports();
  Future<void> saveReport(ReportModel report);
}

class ReportLocalDataSourceImpl implements ReportLocalDataSource {
  @override
  Future<List<ReportModel>> getReports() async {
    final data = await JsonHelper.readJson('reports.json');
    return (data['reports'] as List)
        .map((r) => ReportModel.fromJson(r))
        .toList();
  }

  @override
  Future<void> saveReport(ReportModel report) async {
    final reports = await getReports();
    final newReport = ReportModel(
      reportId: reports.isEmpty ? 1 : reports.last.reportId + 1,
      coachId: report.coachId,
      playerId: report.playerId,
      playerName: report.playerName,
      title: report.title,
      date: report.date,
      performance: report.performance,
      comments: report.comments,
    );
    reports.add(newReport);
    await JsonHelper.writeJson('reports.json', {'reports': reports.map((r) => r.toJson()).toList()});
  }
}
