Để làm rõ từng loại kẻ địch trong game **spaceship shooter**, chúng ta sẽ xây dựng tài liệu và sơ đồ luồng chi tiết cho ba loại kẻ địch chính: **Thuyền**, **Quái vật**, và **Boss**. Mỗi loại sẽ có hành vi, khả năng di chuyển và cách tấn công riêng biệt. 

### 1. Document (Tài liệu)

#### 1.1. Enemy Type: **Thuyền**

- **Mô tả**: Thuyền là loại kẻ địch cơ bản, thường có mặt trong các đợt tấn công với số lượng lớn. Thuyền di chuyển đơn giản, dễ dàng bị tiêu diệt nhưng xuất hiện thường xuyên.
- **Hành vi di chuyển**:
  - **Di chuyển thẳng xuống**: Thuyền di chuyển từ trên xuống dưới với tốc độ không đổi.
  - **Di chuyển Zigzag** (tùy chọn): Một số thuyền có thể di chuyển theo hình zigzag để tăng độ khó.
- **Hành vi tấn công**:
  - **Bắn đơn**: Thuyền có thể bắn một viên đạn thẳng xuống, hướng về phía người chơi.
  - **Không bắn** (tùy chọn): Một số thuyền chỉ làm nhiệm vụ cản đường mà không có khả năng tấn công.
- **Chỉ số**:
  - **Máu**: Thấp (1 hoặc 2 hit là bị tiêu diệt)
  - **Tốc độ**: Trung bình

#### 1.2. Enemy Type: **Quái vật**

- **Mô tả**: Quái vật là loại kẻ địch phức tạp hơn, với sức chịu đựng cao và khả năng tấn công linh hoạt. Chúng thường có các hành vi di chuyển khó đoán.
- **Hành vi di chuyển**:
  - **Theo đuổi người chơi**: Quái vật có khả năng di chuyển về phía người chơi.
  - **Di chuyển theo quỹ đạo tròn**: Một số quái vật di chuyển xung quanh người chơi theo quỹ đạo tròn.
- **Hành vi tấn công**:
  - **Bắn nhiều hướng**: Quái vật có thể bắn nhiều viên đạn ra nhiều hướng.
  - **Bắn đạn truy đuổi** (tùy chọn): Đạn bắn ra sẽ hướng tới vị trí của người chơi.
- **Chỉ số**:
  - **Máu**: Trung bình đến cao
  - **Tốc độ**: Trung bình

#### 1.3. Enemy Type: **Boss**

- **Mô tả**: Boss là loại kẻ địch đặc biệt, xuất hiện ở cuối màn chơi và có sức mạnh vượt trội. Boss thường có nhiều giai đoạn tấn công và là thử thách chính của người chơi.
- **Hành vi di chuyển**:
  - **Di chuyển theo mẫu cố định**: Boss thường di chuyển theo một mẫu định sẵn, ví dụ như di chuyển từ trái qua phải.
  - **Di chuyển theo từng giai đoạn**: Boss có thể thay đổi kiểu di chuyển theo từng giai đoạn (giai đoạn sau khó hơn giai đoạn trước).
- **Hành vi tấn công**:
  - **Bắn đa hướng**: Boss có thể bắn một loạt viên đạn về nhiều hướng.
  - **Bắn đạn truy đuổi**: Một số đạn của boss có thể hướng tới người chơi.
  - **Tạo đạn xoay vòng** (tùy chọn): Đạn bắn ra sẽ tạo thành một vòng tròn, di chuyển về phía người chơi.
- **Chỉ số**:
  - **Máu**: Cao
  - **Tốc độ**: Chậm đến trung bình, tùy thuộc vào giai đoạn

### 2. Flow Diagrams (Sơ đồ luồng)

Dưới đây là các sơ đồ luồng cho từng loại kẻ địch.

#### 2.1. Flow Diagram for **Thuyền**

```plaintext
                +-------------------+
                |   Spawn Thuyền    |
                +-------------------+
                         |
                         |
              +-----------------------+
              | Move Down or Zigzag   |
              +-----------------------+
                         |
                         |
           +----------------------------+
           | Is Player in Shooting Range? |
           +----------------------------+
                         |
            Yes                    No
             |                       |
+------------------------+      +----------------------+
|   Fire Single Bullet   |      |  Continue Moving     |
+------------------------+      +----------------------+
             |
             |
   +------------------+
   |   Thuyền Dies    |
   +------------------+
```

#### 2.2. Flow Diagram for **Quái vật**

```plaintext
                 +---------------------+
                 |   Spawn Quái vật    |
                 +---------------------+
                          |
                          |
           +------------------------------+
           | Move Toward Player or Orbit  |
           +------------------------------+
                          |
                          |
             +------------------------------+
             | Is Player in Shooting Range? |
             +------------------------------+
                          |
               Yes                      No
                |                        |
+--------------------------------+   +----------------------+
|   Fire Multi-Directional Bullets |  | Continue Moving     |
+--------------------------------+   +----------------------+
                |
                |
      +---------------------+
      |   Quái vật Dies     |
      +---------------------+
```

#### 2.3. Flow Diagram for **Boss**

```plaintext
                 +------------------+
                 |   Spawn Boss     |
                 +------------------+
                          |
                          |
           +-------------------------------+
           |  Move Following Pattern       |
           +-------------------------------+
                          |
                          |
                 +-----------------+
                 |   Phase 1      |
                 +-----------------+
                          |
            +---------------------------+
            | Fire Multi-Directional Bullets |
            +---------------------------+
                          |
                 +-----------------+
                 |   Phase 2      |
                 +-----------------+
                          |
            +----------------------------+
            | Fire Targeted Bullets or   |
            | Circular Bullet Patterns   |
            +----------------------------+
                          |
              +--------------------------+
              | Does Boss Health < 0?    |
              +--------------------------+
                          |
                 Yes                   No
                  |                     |
     +-------------------+       +-------------------+
     |     Boss Dies     |       |    Continue      |
     +-------------------+       +-------------------+
```

### Mô tả chi tiết về từng sơ đồ:

- **Thuyền**:
  - Thuyền xuất hiện và di chuyển xuống hoặc theo kiểu zigzag.
  - Khi người chơi ở gần, thuyền bắn một viên đạn thẳng xuống.
  - Nếu không, nó tiếp tục di chuyển cho đến khi ra khỏi màn hình hoặc bị tiêu diệt.

- **Quái vật**:
  - Quái vật xuất hiện và di chuyển về phía người chơi hoặc theo quỹ đạo vòng quanh.
  - Khi ở trong tầm bắn, nó sẽ bắn nhiều viên đạn theo các hướng khác nhau.
  - Quái vật có thể bị tiêu diệt hoặc tiếp tục di chuyển nếu ra khỏi tầm ngắm.

- **Boss**:
  - Boss xuất hiện và di chuyển theo mẫu định trước.
  - Boss có nhiều giai đoạn tấn công, mỗi giai đoạn có các kiểu bắn khác nhau.
  - Khi sức khỏe giảm về 0, boss sẽ bị tiêu diệt, nếu không, boss tiếp tục các giai đoạn tấn công.

### Tổng kết
- **Thuyền**: Là loại kẻ địch dễ tiêu diệt, chủ yếu tạo ra các đợt tấn công cơ bản.
- **Quái vật**: Là loại kẻ địch khó khăn hơn, với các kiểu di chuyển và tấn công phức tạp hơn.
- **Boss**: Là thử thách chính, có sức mạnh vượt trội với nhiều kiểu tấn công và giai đoạn khác nhau, đòi hỏi người chơi phải có kỹ năng tốt để đánh bại.