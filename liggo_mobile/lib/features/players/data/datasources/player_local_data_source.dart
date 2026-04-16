import '../../../../core/utils/json_helper.dart';
import '../models/player_model.dart';

abstract class PlayerLocalDataSource {
  Future<List<PlayerModel>> getPlayers();
}

class PlayerLocalDataSourceImpl implements PlayerLocalDataSource {
  @override
  Future<List<PlayerModel>> getPlayers() async {
    final data = await JsonHelper.readJson('players.json');
    return (data['players'] as List)
        .map((p) => PlayerModel.fromJson(p))
        .toList();
  }
}
