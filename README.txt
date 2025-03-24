- ada 2 program yaitu program API sebagai backend dan program website sebagai frontend

- restore terlebih dahulu database dari file database yang sudah disiapkan nama file nya 'databasenya.bak' ke database lokal

- ubah koneksi di API menjadi koneksi database lokal, ubah koneksi pada file 'DataContext.cs'

- lalu jalankan API di lokal

- kemudian baru jalankan websitenya tapi seblum jalan pastikan sesuaikan pula ubah port di website menjadi localhost API yang sudah jalan sebelumnya pada file 'logincontroller', 'homecontroller' dan file 'View/Home/index.cshtml'