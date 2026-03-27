import 'package:flutter/material.dart';
import 'core/constants/app_colors.dart';
import 'core/utils/json_helper.dart';
import 'features/auth/presentation/pages/splash_page.dart';
import 'injection_container.dart' as di;

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await di.init();
  await JsonHelper.initializeLocalData();
  runApp(const LiggoApp());
}

class LiggoApp extends StatelessWidget {
  const LiggoApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'LIGGO Mobile',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        primaryColor: AppColors.primaryBlue,
        colorScheme: ColorScheme.fromSeed(
          seedColor: AppColors.primaryBlue,
          primary: AppColors.primaryBlue,
          secondary: AppColors.secondaryBlue,
        ),
        scaffoldBackgroundColor: AppColors.background,
        useMaterial3: true,
      ),
      home: const SplashPage(),
    );
  }
}
