import 'package:flutter/material.dart';

import '../../../../core/constants/app_colors.dart';
import '../../../../injection_container.dart';
import '../../domain/repositories/subscription_repository.dart';
import '../../../auth/data/datasources/auth_local_data_source.dart';
import '../../../dashboard/presentation/pages/dashboard_page.dart';

class PaymentPage extends StatefulWidget {
  final int userId;
  final String plan;
  const PaymentPage({super.key, required this.userId, required this.plan});

  @override
  State<PaymentPage> createState() => _PaymentPageState();
}

class _PaymentPageState extends State<PaymentPage> {
  // Datos personales
  final _fullNameController = TextEditingController();
  final _emailController = TextEditingController();
  final _addressController = TextEditingController();
  final _phoneController = TextEditingController();

  // Información financiera
  final _cardNumberController = TextEditingController();
  final _cardExpiryController = TextEditingController();
  final _cardCvvController = TextEditingController();
  final _bankAccountController = TextEditingController();
  final _taxIdController = TextEditingController();

  // Configuración de la cuenta
  String _accountType = 'Personal';
  bool _receiveEmail = true;
  bool _receiveSms = false;
  bool _accessTransactions = true;
  bool _accessContacts = false;

  // Seguridad
  String _documentType = 'INE';
  final _documentNumberController = TextEditingController();
  bool _enable2fa = false;
  bool _phoneVerification = false;

  bool _isProcessing = false;

  @override
  void dispose() {
    _fullNameController.dispose();
    _emailController.dispose();
    _addressController.dispose();
    _phoneController.dispose();
    _cardNumberController.dispose();
    _cardExpiryController.dispose();
    _cardCvvController.dispose();
    _bankAccountController.dispose();
    _taxIdController.dispose();
    _documentNumberController.dispose();
    super.dispose();
  }

  String get _planPrice {
    switch (widget.plan) {
      case 'Academia':
        return '\$1700';
      case 'Individual':
      default:
        return '\$600';
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Pago')),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: SingleChildScrollView(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                _buildHeader(),
                const SizedBox(height: 20),
                _buildSectionTitle('1. Datos personales y de contacto'),
                _buildTextField(_fullNameController, 'Nombre completo', TextInputType.name),
                const SizedBox(height: 12),
                _buildTextField(_emailController, 'Correo electrónico', TextInputType.emailAddress),
                const SizedBox(height: 12),
                _buildTextField(_addressController, 'Dirección física', TextInputType.streetAddress, maxLines: 2),
                const SizedBox(height: 12),
                _buildTextField(_phoneController, 'Teléfono', TextInputType.phone),

                const SizedBox(height: 20),
                _buildSectionTitle('2. Información financiera'),
                _buildTextField(_cardNumberController, 'Número de tarjeta', TextInputType.number),
                const SizedBox(height: 12),
                Row(
                  children: [
                    Expanded(child: _buildTextField(_cardExpiryController, 'Fecha vencimiento', TextInputType.datetime)),
                    const SizedBox(width: 12),
                    Expanded(child: _buildTextField(_cardCvvController, 'CVV', TextInputType.number)),
                  ],
                ),
                const SizedBox(height: 12),
                _buildTextField(_bankAccountController, 'Cuenta bancaria', TextInputType.number),
                const SizedBox(height: 12),
                _buildTextField(_taxIdController, 'RFC / ID fiscal', TextInputType.text),

                const SizedBox(height: 20),
                _buildSectionTitle('3. Configuración de la cuenta y permisos'),
                _buildDropdown(
                  label: 'Tipo de cuenta',
                  value: _accountType,
                  options: const ['Personal', 'Negocios', 'Avanzado'],
                  onChanged: (value) => setState(() => _accountType = value!),
                ),
                const SizedBox(height: 12),
                _buildCheckbox('Recibir facturas por correo', _receiveEmail, (v) => setState(() => _receiveEmail = v ?? false)),
                _buildCheckbox('Recibir notificaciones por SMS', _receiveSms, (v) => setState(() => _receiveSms = v ?? false)),
                const SizedBox(height: 12),
                _buildCheckbox('Permitir acceso a historial de transacciones', _accessTransactions, (v) => setState(() => _accessTransactions = v ?? false)),
                _buildCheckbox('Permitir acceso a contactos', _accessContacts, (v) => setState(() => _accessContacts = v ?? false)),

                const SizedBox(height: 20),
                _buildSectionTitle('4. Seguridad y verificación'),
                _buildDropdown(
                  label: 'Documento de identidad',
                  value: _documentType,
                  options: const ['INE', 'Pasaporte', 'Licencia'],
                  onChanged: (value) => setState(() => _documentType = value!),
                ),
                const SizedBox(height: 12),
                _buildTextField(_documentNumberController, 'Número de documento', TextInputType.text),
                const SizedBox(height: 12),
                _buildCheckbox('Activar autenticación en dos pasos (2FA)', _enable2fa, (v) => setState(() => _enable2fa = v ?? false)),
                _buildCheckbox('Verificación por teléfono o correo', _phoneVerification, (v) => setState(() => _phoneVerification = v ?? false)),

                const SizedBox(height: 24),
                ElevatedButton(
                  onPressed: _isProcessing ? null : _confirmAndPay,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: AppColors.primaryBlue,
                    foregroundColor: Colors.white,
                    padding: const EdgeInsets.symmetric(vertical: 16),
                  ),
                  child: _isProcessing
                      ? const SizedBox(
                          height: 20,
                          width: 20,
                          child: CircularProgressIndicator(color: Colors.white, strokeWidth: 2),
                        )
                      : const Text('Confirmar pago', style: TextStyle(fontSize: 18)),
                ),
                const SizedBox(height: 24),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildHeader() {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text('Resumen del plan', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            const SizedBox(height: 8),
            Text('Plan: ${widget.plan}'),
            Text('Precio: $_planPrice MXN'),
          ],
        ),
      ),
    );
  }

  Widget _buildSectionTitle(String title) {
    return Text(
      title,
      style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
    );
  }

  Widget _buildTextField(TextEditingController controller, String label, TextInputType type,
      {int maxLines = 1}) {
    return TextField(
      controller: controller,
      keyboardType: type,
      maxLines: maxLines,
      decoration: InputDecoration(labelText: label, border: const OutlineInputBorder()),
    );
  }

  Widget _buildDropdown({
    required String label,
    required String value,
    required List<String> options,
    required ValueChanged<String?> onChanged,
  }) {
    return InputDecorator(
      decoration: InputDecoration(labelText: label, border: const OutlineInputBorder()),
      child: DropdownButtonHideUnderline(
        child: DropdownButton<String>(
          value: value,
          isExpanded: true,
          items: options.map((o) => DropdownMenuItem(value: o, child: Text(o))).toList(),
          onChanged: onChanged,
        ),
      ),
    );
  }

  Widget _buildCheckbox(String label, bool value, ValueChanged<bool?> onChanged) {
    return CheckboxListTile(
      contentPadding: EdgeInsets.zero,
      title: Text(label),
      value: value,
      controlAffinity: ListTileControlAffinity.leading,
      onChanged: onChanged,
    );
  }

  Future<void> _confirmAndPay() async {
    if (!_validateFields()) return;

    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Confirmar pago'),
        content: Text('Se realizará el pago de $_planPrice MXN para el plan ${widget.plan}. ¿Deseas continuar?'),
        actions: [
          TextButton(onPressed: () => Navigator.of(context).pop(false), child: const Text('Cancelar')),
          ElevatedButton(onPressed: () => Navigator.of(context).pop(true), child: const Text('Pagar')),
        ],
      ),
    );

    if (confirmed != true) return;
    if (!mounted) return;

    setState(() => _isProcessing = true);
    FocusScope.of(context).unfocus();

    final repository = sl<SubscriptionRepository>();
    final selectResult = await repository.selectPlan(widget.userId, widget.plan);

    if (!mounted) return;

    if (selectResult.isLeft()) {
      setState(() => _isProcessing = false);
      selectResult.fold((failure) => ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(failure.message))), (_) {});
      return;
    }

    final paymentResult = await repository.completePayment(widget.userId);

    if (!mounted) return;
    setState(() => _isProcessing = false);

    paymentResult.fold(
      (failure) => ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(failure.message))),
      (_) async {
        if (!mounted) return;
        final user = await sl<AuthLocalDataSource>().getUserById(widget.userId);
        if (!mounted) return;
        if (user != null) {
          Navigator.pushAndRemoveUntil(
            context,
            MaterialPageRoute(builder: (context) => DashboardPage(user: user)),
            (route) => false,
          );
        } else {
          ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('No se encontró el usuario después del pago.')));
        }
      },
    );
  }

  bool _validateFields() {
    if (_fullNameController.text.trim().isEmpty) {
      _showError('Completa tu nombre completo');
      return false;
    }
    if (!_emailController.text.contains('@')) {
      _showError('Ingresa un correo válido');
      return false;
    }
    if (_addressController.text.trim().isEmpty) {
      _showError('Completa tu dirección');
      return false;
    }
    if (_phoneController.text.trim().length < 7) {
      _showError('Ingresa un teléfono válido');
      return false;
    }
    if (_cardNumberController.text.trim().length < 12) {
      _showError('Ingresa un número de tarjeta válido');
      return false;
    }
    if (_cardExpiryController.text.trim().isEmpty) {
      _showError('Ingresa fecha de vencimiento');
      return false;
    }
    if (_cardCvvController.text.trim().length < 3) {
      _showError('Ingresa el código de seguridad');
      return false;
    }
    return true;
  }

  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(message)));
  }
}
