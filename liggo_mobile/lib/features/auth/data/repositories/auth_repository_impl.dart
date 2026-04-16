import 'package:dartz/dartz.dart';
import '../../../../core/errors/failures.dart';
import '../../domain/entities/user.dart';
import '../../domain/repositories/auth_repository.dart';
import '../datasources/auth_local_data_source.dart';
import '../models/user_model.dart';

class AuthRepositoryImpl implements AuthRepository {
  final AuthLocalDataSource localDataSource;

  AuthRepositoryImpl({required this.localDataSource});

  @override
  Future<Either<Failure, User>> login(String email, String password) async {
    try {
      final user = await localDataSource.login(email, password);
      if (user != null) {
        return Right(user);
      } else {
        return const Left(AuthFailure("Credenciales incorrectas"));
      }
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<Either<Failure, User>> register(String nombre, String email, String password, String rol) async {
    try {
      final userModel = UserModel(
        id: 0,
        nombre: nombre,
        email: email,
        password: password,
        rol: rol,
        planActivo: false,
      );
      final result = await localDataSource.register(userModel);
      return Right(result);
    } catch (e) {
      return const Left(ServerFailure());
    }
  }

  @override
  Future<void> logout() async {
    // Implement logout logic if needed (e.g. clearing session)
  }
}
