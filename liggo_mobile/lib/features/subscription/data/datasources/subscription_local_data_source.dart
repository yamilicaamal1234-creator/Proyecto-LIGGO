import '../../../../core/utils/json_helper.dart';
import '../models/subscription_model.dart';

abstract class SubscriptionLocalDataSource {
  Future<SubscriptionModel?> getSubscription(int userId);
  Future<void> saveSubscription(SubscriptionModel subscription);
  Future<void> updateSubscriptionStatus(int userId, String status);
}

class SubscriptionLocalDataSourceImpl implements SubscriptionLocalDataSource {
  @override
  Future<SubscriptionModel?> getSubscription(int userId) async {
    final data = await JsonHelper.readJson('subscriptions.json');
    final subscriptions = (data['subscriptions'] as List)
        .map((s) => SubscriptionModel.fromJson(s))
        .toList();
    
    try {
      return subscriptions.firstWhere((s) => s.userId == userId);
    } catch (e) {
      return null;
    }
  }

  @override
  Future<void> saveSubscription(SubscriptionModel subscription) async {
    final data = await JsonHelper.readJson('subscriptions.json');
    final subscriptions = (data['subscriptions'] as List)
        .map((s) => SubscriptionModel.fromJson(s))
        .toList();
    
    final index = subscriptions.indexWhere((s) => s.userId == subscription.userId);
    if (index != -1) {
      subscriptions[index] = subscription;
    } else {
      subscriptions.add(subscription);
    }
    
    await JsonHelper.writeJson('subscriptions.json', {'subscriptions': subscriptions.map((s) => s.toJson()).toList()});
  }

  @override
  Future<void> updateSubscriptionStatus(int userId, String status) async {
    final sub = await getSubscription(userId);
    if (sub != null) {
      await saveSubscription(SubscriptionModel(
        userId: userId,
        plan: sub.plan,
        status: status,
      ));
    }
  }
}
