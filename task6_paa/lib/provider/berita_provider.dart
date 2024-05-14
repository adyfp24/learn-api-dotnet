import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:task6_paa/model/berita.dart';

class BeritaProvider extends ChangeNotifier {
  List<Berita> _berita = [];
  List<Berita> get berita => _berita;

  bool _isLoading = false;
  bool get isLoading => _isLoading;

  String baseUrl = "https://api-berita-indonesia.vercel.app/antara/bola/";

  Future<void> fetchData() async {
    _isLoading = true;
    notifyListeners();
    try {
      final response = await http.get(Uri.parse(baseUrl));
      if (response.statusCode == 200) {
        final data = json.decode(response.body)['data']['posts'];
        final List<dynamic> posts = data;
        _berita = posts.map((post) => Berita.fromJson(post)).toList();
      } else {
        print('Failed to load data');
      }
    } catch (e) {
      print('error fetching data :' + e.toString());
    }
    _isLoading = false;
    notifyListeners();
  }
}
