import '../../domain/entities/user.dart';

class UserModel extends User {
  const UserModel({
    required super.id,
    required super.nombre,
    required super.email,
    required super.password,
    required super.rol,
    required super.planActivo,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'],
      nombre: json['nombre'],
      email: json['email'],
      password: json['password'],
      rol: json['rol'],
      planActivo: json['planActivo'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'nombre': nombre,
      'email': email,
      'password': password,
      'rol': rol,
      'planActivo': planActivo,
    };
  }
}
