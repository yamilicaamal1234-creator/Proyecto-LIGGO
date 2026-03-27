import 'package:equatable/equatable.dart';

class User extends Equatable {
  final int id;
  final String nombre;
  final String email;
  final String password;
  final String rol;
  final bool planActivo;

  const User({
    required this.id,
    required this.nombre,
    required this.email,
    required this.password,
    required this.rol,
    required this.planActivo,
  });

  @override
  List<Object?> get props => [id, nombre, email, password, rol, planActivo];
}
