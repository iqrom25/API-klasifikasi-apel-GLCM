# API-klasifikasi-apel-GLCM

## Anonymous Message API

Project ini merupakan REST API bertugas untuk mengelola mengklasifikasi apel menggunakan metode GLCM
### Technologies

- mysql
- .NET 6 API

### API Spec

#### List Data Latih

- Request: GET
- Endpoint : `/api/v1/CitraDigital/ListDataLatih`
- Header :
  - Content-Type: application/json
  - Accept: application/json

Response:

```json
{
  "status": String,
  "message": String,
  "data": [
    {
      "id": Number,
      "kelas": String,
      "red": Number,
      "green": Number,
      "blue": Number,
      "energi": Number,
      "kontras": Number,
      "homogenitas": Number,
      "korelasi": Number,
      "sudut": String
    },
}
```

#### Pelatihan

- Request: POST
- Endpoint : `/api/v1/CitraDigital/Pelatihan`
- Header :
  - Content-Type: multipart/form-data
  - Accept: multipart/form-data
- Body :

```json
{
  "DataLatih": [File],
  "JenisPelatihan": String
  "Sudut": String
}
```

Response:

```json
{
 {
  "status": String,
  "message": String,
  "data": [
    {
      "id": Number,
      "kelas": String,
      "red": Number,
      "green": Number,
      "blue": Number,
      "energi": Number,
      "kontras": Number,
      "homogenitas": Number,
      "korelasi": Number,
      "sudut": String
    }
  ]
}
}
```

#### Pengujian

- Request: POST
- Endpoint : `/api/v1/CitraDigital/Pengujian`
- Header :
  - Content-Type: multipart/form-data
  - Accept: multipart/form-data
- Body :

```json
{
  "Gambar": File,
  "Sudut": String
}
```

Response:

```json
{
 {
  "status": String,
  "message": String,
  "data": {
    "hasil": {
      "kelas": String,
      "red": Number,
      "green": Number,
      "blue": Number,
      "glcm": {
        "energi": Number,
        "kontras": Number,
        "homogenitas": Number,
        "korelasi": Number
      }
    },
    "grayscale": String (Base64)
    "tetanggaTerdekat": [
      {
        "dataLatih": {
          "id": Number,
          "kelas": String,
          "red": Number,
          "green": Number,
          "blue": Number,
          "energi": Number,
          "kontras": Number,
          "homogenitas": Number,
          "korelasi": Number,
          "sudut": String
        },
        "jarak": Number
      },
      {
        "dataLatih": {
          "id": Number,
          "kelas": String,
          "red": Number,
          "green": Number,
          "blue": Number,
          "energi": Number,
          "kontras": Number,
          "homogenitas": Number,
          "korelasi": Number,
          "sudut": String
        },
        "jarak": Number
      },
      {
        "dataLatih": {
          "id": Number,
          "kelas": String,
          "red": Number,
          "green": Number,
          "blue": Number,
          "energi": Number,
          "kontras": Number,
          "homogenitas": Number,
          "korelasi": Number,
          "sudut": String
        },
        "jarak": Number
      }
    ]
}
}
```

