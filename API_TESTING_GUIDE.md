# 🧪 API Testing Guide - Netflix Clone

## Prerequisites
1. ✅ API is running (Press F5 in Visual Studio)
2. ✅ Swagger UI is open at `https://localhost:7093` (or your port)
3. ✅ Database migration completed
4. ✅ At least 1 movie created (use POST /api/movies first)

---

## Test Sequence (Follow in Order)

### 1️⃣ Health Check
**Endpoint:** `GET /api/health`

**Expected Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2026-02-16T10:36:23Z",
  "service": "Netflix Clone API",
  "version": "1.0.0"
}
```

---

### 2️⃣ Create Movies (Do this first!)
**Endpoint:** `POST /api/movies`

**Test Data 1:**
```json
{
  "title": "Inception",
  "description": "A thief who steals corporate secrets through dream-sharing technology",
  "releaseYear": 2010,
  "durationMinutes": 148,
  "videoUrl": "https://example.com/inception.mp4",
  "thumbnailUrl": "https://example.com/inception-thumb.jpg",
  "bannerUrl": "https://example.com/inception-banner.jpg",
  "rating": 3,
  "director": "Christopher Nolan",
  "cast": "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
  "genres": [0, 1]
}
```

**Test Data 2:**
```json
{
  "title": "The Dark Knight",
  "description": "Batman faces the Joker in Gotham City",
  "releaseYear": 2008,
  "durationMinutes": 152,
  "videoUrl": "https://example.com/dark-knight.mp4",
  "thumbnailUrl": "https://example.com/dark-knight-thumb.jpg",
  "bannerUrl": "https://example.com/dark-knight-banner.jpg",
  "rating": 3,
  "director": "Christopher Nolan",
  "cast": "Christian Bale, Heath Ledger, Aaron Eckhart",
  "genres": [0, 1]
}
```

**Expected Response:** `201 Created`
```json
{
  "id": "guid-here"
}
```

**Save the movie IDs** - you'll need them for other tests!

---

### 3️⃣ Get All Movies
**Endpoint:** `GET /api/movies`

**Expected Response:** `200 OK`
```json
[
  {
    "id": "guid",
    "title": "Inception",
    "description": "...",
    "releaseYear": 2010,
    "durationMinutes": 148,
    "thumbnailUrl": "...",
    "rating": 3,
    "averageRating": 0,
    "genres": [0, 1]
  }
]
```

---

### 4️⃣ Search Movies
**Endpoint:** `GET /api/movies/search?term=inception`

**Test Queries:**
- `?term=inception` - Should find "Inception"
- `?term=nolan` - Should find both movies (director)
- `?term=batman` - Should find "The Dark Knight"

**Expected Response:** `200 OK` with matching movies

---

### 5️⃣ Get Popular Movies
**Endpoint:** `GET /api/movies/popular?count=10`

**Expected Response:** `200 OK` with movies sorted by popularity

---

### 6️⃣ Update Watch History
**Endpoint:** `POST /api/watchhistory`

**Test Data:**
```json
{
  "userId": "00000000-0000-0000-0000-000000000001",
  "movieId": "YOUR-MOVIE-ID-HERE",
  "lastWatchedPositionSeconds": 3600,
  "movieDurationSeconds": 8880
}
```

**Replace `YOUR-MOVIE-ID-HERE`** with actual movie ID from step 2!

**Expected Response:** `200 OK`
```json
{
  "message": "Watch history updated successfully"
}
```

---

### 7️⃣ Get Continue Watching
**Endpoint:** `GET /api/watchhistory/continue-watching?userId=00000000-0000-0000-0000-000000000001&count=10`

**Expected Response:** `200 OK`
```json
[
  {
    "id": "guid",
    "userId": "00000000-0000-0000-0000-000000000001",
    "movieId": "guid",
    "lastWatchedPositionSeconds": 3600,
    "percentageWatched": 40.54,
    "isCompleted": false,
    "lastWatchedAt": "2026-02-16T10:36:23Z",
    "movie": {
      "id": "guid",
      "title": "Inception",
      ...
    }
  }
]
```

---

### 8️⃣ Add to My List
**Endpoint:** `POST /api/mylist`

**Test Data:**
```json
{
  "userId": "00000000-0000-0000-0000-000000000001",
  "movieId": "YOUR-MOVIE-ID-HERE"
}
```

**Expected Response:** `200 OK`
```json
{
  "id": "guid",
  "message": "Movie added to My List"
}
```

---

### 9️⃣ Get My List
**Endpoint:** `GET /api/mylist?userId=00000000-0000-0000-0000-000000000001`

**Expected Response:** `200 OK`
```json
[
  {
    "id": "guid",
    "title": "Inception",
    "description": "...",
    ...
  }
]
```

---

### 🔟 Remove from My List
**Endpoint:** `DELETE /api/mylist/{movieId}?userId=00000000-0000-0000-0000-000000000001`

**Replace `{movieId}`** with actual movie ID!

**Expected Response:** `200 OK`
```json
{
  "message": "Movie removed from My List"
}
```

---

### 1️⃣1️⃣ Create Watch Party Room
**Endpoint:** `POST /api/rooms`

**Test Data:**
```json
{
  "name": "Movie Night with Friends",
  "hostUserId": "00000000-0000-0000-0000-000000000001",
  "movieId": "YOUR-MOVIE-ID-HERE"
}
```

**Expected Response:** `200 OK`
```json
{
  "roomCode": "ABC123",
  "message": "Room created successfully"
}
```

**Save the room code** for joining later!

---

### 1️⃣2️⃣ Hangfire Dashboard
**URL:** `https://localhost:7093/hangfire`

**Expected:** Dashboard UI showing:
- Jobs
- Recurring Jobs
- Servers
- Retries
- Succeeded/Failed jobs

---

## 🎯 Quick Test Checklist

- [ ] Health check returns 200
- [ ] Can create movies (POST)
- [ ] Can get all movies (GET)
- [ ] Can search movies
- [ ] Can get popular movies
- [ ] Can update watch history
- [ ] Can get continue watching list
- [ ] Can add to My List
- [ ] Can get My List
- [ ] Can remove from My List
- [ ] Can create watch party room
- [ ] Hangfire dashboard loads

---

## 🐛 Common Issues

### Issue: "Movie not found"
**Solution:** Make sure you created movies first (Step 2)

### Issue: "No service for type IRequestHandler"
**Solution:** Rebuild the solution - handlers not registered

### Issue: Empty arrays returned
**Solution:** Normal if no data exists yet - create test data first

### Issue: 400 Bad Request
**Solution:** Check JSON format and required fields

---

## 📝 Test User ID

For all tests, use this test user ID:
```
00000000-0000-0000-0000-000000000001
```

(In production, this would come from authentication)

---

## ✅ Success Criteria

All endpoints should return:
- ✅ Correct HTTP status codes (200, 201, 400, etc.)
- ✅ Valid JSON responses
- ✅ Data persisted in database
- ✅ No exceptions in console

---

**Ready to test? Start with Step 1 (Health Check) and work your way down!** 🚀
