import '../../../../core/utils/json_helper.dart';
import '../models/payment_model.dart';

abstract class PaymentLocalDataSource {
  Future<List<PaymentModel>> getPayments();
}

class PaymentLocalDataSourceImpl implements PaymentLocalDataSource {
  @override
  Future<List<PaymentModel>> getPayments() async {
    try {
      final data = await JsonHelper.readJson('payments.json');
      if (data.isEmpty || data['payments'] == null) return [];
      return (data['payments'] as List)
          .map((p) => PaymentModel.fromJson(p))
          .toList();
    } catch (e) {
      return [];
    }
  }
}
