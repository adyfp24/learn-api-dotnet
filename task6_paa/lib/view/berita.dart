import 'package:flutter/material.dart';

class BeritaPage extends StatelessWidget {
  const BeritaPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('API Berita', style: TextStyle(color: Colors.white),),
      ),
      body: ListView.builder(itemBuilder: , )
    );
  }
}
