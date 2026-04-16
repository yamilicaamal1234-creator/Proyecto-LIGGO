import '../../../../core/utils/json_helper.dart';
import '../models/user_model.dart';

abstract class AuthLocalDataSource {
  Future<UserModel?> login(String email, String password);
  Future<UserModel> register(UserModel user);
  Future<void> saveUser(UserModel user);
  Future<UserModel?> getUserById(int id);
  Future<void> updateUserPlan(int id, bool active);
}

class AuthLocalDataSourceImpl implements AuthLocalDataSource {
  @override
  Future<UserModel?> getUserById(int id) async {
    final data = await JsonHelper.readJson('users.json');
    final users = (data['users'] as List)
        .map((u) => UserModel.fromJson(u))
        .toList();
    
    try {
      return users.firstWhere((u) => u.id == id);
    } catch (e) {
      return null;
    }
  }

  @override
  Future<void> updateUserPlan(int id, bool active) async {
    final user = await getUserById(id);
    if (user != null) {
      final updatedUser = UserModel(
        id: user.id,
        nombre: user.nombre,
        email: user.email,
        password: user.password,
        rol: user.rol,
        planActivo: active,
      );
      await saveUser(updatedUser);
    }
  }

  @override
  Future<UserModel?> login(String email, String password) async {
    final data = await JsonHelper.readJson('users.json');
    final users = (data['users'] as List)
        .map((u) => UserModel.fromJson(u))
        .toList();
    
    try {
      return users.firstWhere((u) => u.email == email && u.password == password);
    } catch (e) {
      return null;
    }
  }

  @override
  Future<UserModel> register(UserModel user) async {
    final data = await JsonHelper.readJson('users.json');
    final users = (data['users'] as List)
        .map((u) => UserModel.fromJson(u))
        .toList();
    
    final newUser = UserModel(
      id: users.isEmpty ? 1 : users.last.id + 1,
      nombre: user.nombre,
      email: user.email,
      password: user.password,
      rol: user.rol,
      planActivo: false,
    );

    users.add(newUser);
    await JsonHelper.writeJson('users.json', {'users': users.map((u) => u.toJson()).toList()});
    return newUser;
  }

  @override
  Future<void> saveUser(UserModel user) async {
    final data = await JsonHelper.readJson('users.json');
    final users = (data['users'] as List)
        .map((u) => UserModel.fromJson(u))
        .toList();
    
    final index = users.indexWhere((u) => u.id == user.id);
    if (index != -1) {
      users[index] = user;
    } else {
      users.add(user);
    }
    
    await JsonHelper.writeJson('users.json', {'users': users.map((u) => u.toJson()).toList()});
  }
}
