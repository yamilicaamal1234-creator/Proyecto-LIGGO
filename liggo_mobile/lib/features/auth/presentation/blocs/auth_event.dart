import 'package:equatable/equatable.dart';

abstract class AuthEvent extends Equatable {
  const AuthEvent();
  @override
  List<Object?> get props => [];
}

class LoginRequested extends AuthEvent {
  final String email;
  final String password;
  const LoginRequested(this.email, this.password);
  @override
  List<Object?> get props => [email, password];
}

class RegisterRequested extends AuthEvent {
  final String nombre;
  final String email;
  final String password;
  final String rol;
  const RegisterRequested(this.nombre, this.email, this.password, this.rol);
  @override
  List<Object?> get props => [nombre, email, password, rol];
}

class LogoutRequested extends AuthEvent {}
