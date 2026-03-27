import '../../../../core/utils/json_helper.dart';
import '../models/incident_model.dart';

abstract class IncidentLocalDataSource {
  Future<List<IncidentModel>> getIncidents();
  Future<void> saveIncident(IncidentModel incident);
}

class IncidentLocalDataSourceImpl implements IncidentLocalDataSource {
  @override
  Future<List<IncidentModel>> getIncidents() async {
    try {
      final data = await JsonHelper.readJson('incidents.json');
      if (data.isEmpty || data['incidents'] == null) return [];
      return (data['incidents'] as List)
          .map((i) => IncidentModel.fromJson(i))
          .toList();
    } catch (e) {
      return [];
    }
  }

  @override
  Future<void> saveIncident(IncidentModel incident) async {
    final incidents = await getIncidents();
    final newIncident = IncidentModel(
      id: incidents.isEmpty ? 1 : incidents.last.id + 1,
      type: incident.type,
      description: incident.description,
      date: incident.date,
      playerId: incident.playerId,
    );
    incidents.add(newIncident);
    await JsonHelper.writeJson('incidents.json', {'incidents': incidents.map((i) => i.toJson()).toList()});
  }
}
