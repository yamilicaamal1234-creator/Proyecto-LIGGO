import '../../../../core/utils/json_helper.dart';
import '../models/match_model.dart';

abstract class MatchLocalDataSource {
  Future<List<MatchModel>> getMatches();
}

class MatchLocalDataSourceImpl implements MatchLocalDataSource {
  @override
  Future<List<MatchModel>> getMatches() async {
    try {
      final data = await JsonHelper.readJson('matches.json');
      if (data.isEmpty || data['matches'] == null) return [];
      return (data['matches'] as List)
          .map((m) => MatchModel.fromJson(m))
          .toList();
    } catch (e) {
      return [];
    }
  }
}
