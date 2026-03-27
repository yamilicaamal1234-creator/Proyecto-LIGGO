import 'package:get_it/get_it.dart';
import 'features/auth/data/datasources/auth_local_data_source.dart';
import 'features/auth/data/repositories/auth_repository_impl.dart';
import 'features/auth/domain/repositories/auth_repository.dart';
import 'features/auth/domain/usecases/login_usecase.dart';
import 'features/auth/domain/usecases/register_usecase.dart';
import 'features/auth/presentation/blocs/auth_bloc.dart';
import 'features/subscription/data/datasources/subscription_local_data_source.dart';
import 'features/subscription/data/repositories/subscription_repository_impl.dart';
import 'features/subscription/domain/repositories/subscription_repository.dart';
import 'features/reports/data/datasources/report_local_data_source.dart';
import 'features/reports/data/repositories/report_repository_impl.dart';
import 'features/reports/domain/repositories/report_repository.dart';
import 'features/players/data/datasources/player_local_data_source.dart';
import 'features/players/data/repositories/player_repository_impl.dart';
import 'features/players/domain/repositories/player_repository.dart';
import 'features/incidents/data/datasources/incident_local_data_source.dart';
import 'features/incidents/data/repositories/incident_repository_impl.dart';
import 'features/incidents/domain/repositories/incident_repository.dart';
import 'features/attendance/data/datasources/attendance_local_data_source.dart';
import 'features/attendance/data/repositories/attendance_repository_impl.dart';
import 'features/attendance/domain/repositories/attendance_repository.dart';
import 'features/matches/data/datasources/match_local_data_source.dart';
import 'features/matches/data/repositories/match_repository_impl.dart';
import 'features/matches/domain/repositories/match_repository.dart';
import 'features/payments/data/datasources/payment_local_data_source.dart';
import 'features/payments/data/repositories/payment_repository_impl.dart';
import 'features/payments/domain/repositories/payment_repository.dart';

final sl = GetIt.instance;

Future<void> init() async {
  // Features - Auth
  sl.registerFactory(() => AuthBloc(loginUseCase: sl(), registerUseCase: sl()));
  sl.registerLazySingleton(() => LoginUseCase(sl()));
  sl.registerLazySingleton(() => RegisterUseCase(sl()));
  sl.registerLazySingleton<AuthRepository>(() => AuthRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<AuthLocalDataSource>(() => AuthLocalDataSourceImpl());

  // Features - Subscription
  sl.registerLazySingleton<SubscriptionRepository>(() => SubscriptionRepositoryImpl(localDataSource: sl(), authLocalDataSource: sl()));
  sl.registerLazySingleton<SubscriptionLocalDataSource>(() => SubscriptionLocalDataSourceImpl());

  // Features - Reports
  sl.registerLazySingleton<ReportRepository>(() => ReportRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<ReportLocalDataSource>(() => ReportLocalDataSourceImpl());

  // Features - Players
  sl.registerLazySingleton<PlayerRepository>(() => PlayerRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<PlayerLocalDataSource>(() => PlayerLocalDataSourceImpl());

  // Features - Incidents
  sl.registerLazySingleton<IncidentRepository>(() => IncidentRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<IncidentLocalDataSource>(() => IncidentLocalDataSourceImpl());

  // Features - Attendance
  sl.registerLazySingleton<AttendanceRepository>(() => AttendanceRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<AttendanceLocalDataSource>(() => AttendanceLocalDataSourceImpl());

  // Features - Matches
  sl.registerLazySingleton<MatchRepository>(() => MatchRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<MatchLocalDataSource>(() => MatchLocalDataSourceImpl());

  // Features - Payments
  sl.registerLazySingleton<PaymentRepository>(() => PaymentRepositoryImpl(localDataSource: sl()));
  sl.registerLazySingleton<PaymentLocalDataSource>(() => PaymentLocalDataSourceImpl());
}
