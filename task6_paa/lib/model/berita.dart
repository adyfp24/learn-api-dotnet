class Berita {
  final String link;
  final String title;
  final String pubDate;
  final String description;
  final String thumbnail;

  Berita(
    this.link,
    this.title,
    this.pubDate,
    this.description,
    this.thumbnail,
  );

  factory Berita.fromJson(Map<String, dynamic> json) {
    return Berita(
      json['link'],
      json['title'],
      json['pubDate'],
      json['description'],
      json['thumbnail'],
    );
  }
}
