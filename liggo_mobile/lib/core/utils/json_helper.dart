import 'dart:convert';
import 'dart:io';
import 'package:flutter/services.dart';
import 'package:path_provider/path_provider.dart';

class JsonHelper {
  static Future<void> initializeLocalData() async {
    final directory = await getApplicationDocumentsDirectory();
    final jsonFiles = [
      'users.json',
      'subscriptions.json',
      'players.json',
      'reports.json',
      'incidents.json',
      'attendance.json',
      'matches.json',
      'payments.json'
    ];

    for (final fileName in jsonFiles) {
      try {
        final file = File('${directory.path}/$fileName');
        // If it doesn't exist, we copy it from assets
        if (!await file.exists()) {
          final content = await rootBundle.loadString('assets/json/$fileName');
          await file.writeAsString(content);
        } else {
          // Verify if it's readable, if not, overwrite with asset
          try {
            final content = await file.readAsString();
            json.decode(content);
          } catch (e) {
            final content = await rootBundle.loadString('assets/json/$fileName');
            await file.writeAsString(content);
          }
        }
      } catch (e) {
        print('Error initializing $fileName: $e');
      }
    }
  }

  static Future<Map<String, dynamic>> readJson(String fileName) async {
    try {
      final directory = await getApplicationDocumentsDirectory();
      final file = File('${directory.path}/$fileName');
      if (await file.exists()) {
        final content = await file.readAsString();
        return json.decode(content);
      }
    } catch (e) {
      print('Error reading $fileName: $e');
    }
    return {};
  }

  static Future<void> writeJson(String fileName, Map<String, dynamic> data) async {
    try {
      final directory = await getApplicationDocumentsDirectory();
      final file = File('${directory.path}/$fileName');
      await file.writeAsString(json.encode(data));
    } catch (e) {
      print('Error writing $fileName: $e');
    }
  }
}
