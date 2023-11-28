import pygame, sys, random
from random import randint
from pygame import mixer
from pygame.locals import *
# Khởi động pygame
pygame.init()

mixer.music.load("bgsound.wav")
mixer.music.play(-1)

# Lâý nhiều event khi ấn giữ phím
pygame.key.set_repeat(120, 0)
# Thiết lập kích thước màn hình chính của game
DISPLAY_width = 800
DISPLAY_height = 960
DISPLAY = [DISPLAY_width, DISPLAY_height]
SURFACE = pygame.display.set_mode(DISPLAY)
# Thiết lập tên của game
pygame.display.set_caption("空戦")
# Thiết lập FPS
FPSCLOCK = pygame.time.Clock()
# Tải các hình ảnh trong game
plane_image = pygame.image.load("plane.png")
enemy_image = pygame.image.load("enemy1.png")
enemy_ammo = pygame.image.load("enemyshot.png")
plane_ammo = pygame.image.load("shot.png")
BACKGROUND1 = pygame.image.load("Blue Sky1.png")
BACKGROUND2 = pygame.image.load("Blue Sky1.png")

# Danh sách tọa độ của đạn 
list_Ammo = []
list_EnemyAmmo = []
list_Enemy = []
#Thiết lập tốc độ đạn của người chơi
Ammo_speed = 15
# Màu sắc cơ bản
black = (0, 0, 0)
white = (255, 255, 255)
red = (255, 0, 0)
yellow = (255, 255, 0)

#Class xử lý ảnh nền
class Background():
    # Hàm vẽ chuyển động nền game theo kích thước màn hình chính
    def draw(self, POS1, POS2):
        self.POS1 = POS1
        self.POS2 = POS2
        while POS1[1] < 961:
            SURFACE.blit(BACKGROUND1, POS1)
            if  POS1[1] == 960:
                POS1[1] = 0
            else:
                POS1[1] += 5
            break
        while POS2[1] < 6:
            SURFACE.blit(BACKGROUND2, POS2)
            if  POS2[1] == 5:
                POS2[1] = -955
            else:
                POS2[1] += 5  
            break

# Class nhân vật
class Character():
    # Hàm vẽ máy bay người chơi
    def draw_Plane(self, plane_pos):
        self.plane_pos = plane_pos
        SURFACE.blit(plane_image, (plane_pos))

    # Hàm vẽ đạn của người chơi
    def draw_Plane_Ammo(self, enemy_pos):
        for count, i in enumerate(list_Ammo):
            # Lấy tọa độ đạn trong danh sách
            xAmmo = i["xAmmo"]
            yAmmo = i["yAmmo"]
            # Hiển thị ra màn hình
            SURFACE.blit(plane_ammo, (xAmmo, yAmmo))
            #Thiết lập hướng di chuyển của đạn lên trên
            list_Ammo[count]["yAmmo"] = yAmmo - Ammo_speed
            # Nếu đạn gần ra ngoài màn hình hoặc chạm vào địch thì xóa đạn
            if yAmmo <= 1 or collision(plane_ammo, (xAmmo, yAmmo), enemy_image, enemy_pos) == True:
                list_Ammo.remove(list_Ammo[count])
        #Thử xem hoạt động liên quan
        #print(enemy_pos)
            #print(xAmmo, yAmmo)
        
    # Hàm vẽ máy bay địch
    def draw_Enemy(self, plane_pos, Enemy_Speed):
        for count, i in enumerate(list_Enemy):
            xEnemy = i["xEnemy"]
            yEnemy = i["yEnemy"]
            SURFACE.blit(enemy_image, (xEnemy, yEnemy))
            #print(xEnemy, yEnemy)
            list_Enemy[count]["xEnemy"] = xEnemy + Enemy_Speed
            if xEnemy >= 799 or collision(plane_image, plane_pos, enemy_image, (xEnemy, yEnemy)) == True:
                list_Enemy.remove(list_Enemy[count])
        #print(list_Enemy)

    # Hàm vẽ đạn của địch
    def draw_Enemy_Ammo(self, plane_pos, EnemyAmmo_speed):
        for count, i in enumerate(list_EnemyAmmo):
            # Lấy tọa độ đạn của địch trong danh sách
            xEnemyAmmo = i["xEnemyAmmo"]
            yEnemyAmmo = i["yEnemyAmmo"]
            # Hiển thị ra màn hình
            SURFACE.blit(enemy_ammo, (xEnemyAmmo, yEnemyAmmo))
            #Thiết lập hướng di chuyển của đạn xuống dưới
            list_EnemyAmmo[count]["yEnemyAmmo"] = yEnemyAmmo + EnemyAmmo_speed
            #Nếu đạn gần ra ngoài màn hình hoặc chạm vào người chơi thì xóa đạn
            if yEnemyAmmo >= 959 or collision(plane_image, plane_pos, enemy_ammo, (xEnemyAmmo, yEnemyAmmo)) == True:
                list_EnemyAmmo.remove(list_EnemyAmmo[count])
                sound("laser2.wav")
        #print(list_EnemyAmmo)

#Hàm âm thanh
def sound(link):
    sound = mixer.Sound(link)
    sound.play()

#Hàm lấy tọa độ trung tâm
def GetCenter(image, pos):
    x = pos[0] + image.get_width() // 2
    y = pos[1] + image.get_height() // 2
    center = (x, y)
    return center

#Hàm hiển thị thông báo
def show_message(x, y, text, size):
    font = pygame.font.SysFont("None", size)
    message = font.render(str(text), True, red, yellow)
    SURFACE.blit(message, (x, y))

# Hàm xủ lý va chạm của 2 đối tượng dựa vào vị trí của nó (pos1, pos2)
def collision(surface1, pos1, surface2, pos2):
    mask1 = pygame.mask.from_surface(surface1)
    mask2 = pygame.mask.from_surface(surface2)
    x = pos2[0] - pos1[0]
    y = pos2[1] - pos1[1]
    #print(x, y)
    if mask1.overlap(mask2, (x, y)) != None:
        return True
    return False

# Hàm chính của game
def main():
    # Thiết lập các đối tượng
    background = Background()
    character = Character()
    ### Thiết lập các giá trị ban đầu
    #Vị trí ban đầu của 2 ảnh nền chuyển động
    POS1, POS2 = [0, 0], [0, -955]
    # Vị trí ban đầu của máy bay người chơi
    plane_pos = [400, 800]
    # Số lượng máy bay địch tối đa
    Number_Enemy = 1
    # Số lượng đạn của địch tối đa trên màn hình
    Number_EnemyAmmo = 1
    # Tốc độ đạn của địch
    EnemyAmmo_speed = 15
    # Tốc độ máy bay địch
    Enemy_Speed = 5
    # Điểm ban đầu
    Score = 0
    # Diểm cao 
    High_Score = 0
    # Thiết lập trạng thái Game Over
    Game_Over = False
    Game_Over_Music = True

    # Trạng thái bật tắt hướng dẫn
    guide = True

    # Vòng lặp chính của game
    while True:
        # Lấy và thao tác với các event trong game
        for event in pygame.event.get():
            # Khi nhấn nút thoát có thể thoát game
            if event.type == QUIT:
                pygame.quit()
                sys.exit()
            # Máy bay người chơi di chuyển theo vị trí chuột
            elif event.type == MOUSEMOTION:
                # Phạm vi di chuyển trên màn hình
                if (event.pos[0] in range(0, DISPLAY[0]- 15)) and  (event.pos[1] in range(300, DISPLAY[1]- 18)):
                    plane_pos = event.pos 

            # Thiết lập vị trí của đạn trước khi bắn luôn ở trung tâm máy bay kể cả khi máy bay di chuyển
            # Lấy trạng thái của chuột trái
            elif pygame.mouse.get_pressed()[0]:
                # Khi chưa Game Over
                if (Game_Over == False):
                    #Thiết lập vị trí đạn ở trung tâm máy bay
                    AmmoStart = GetCenter(plane_image, plane_pos)
                    list_Ammo.append({
                        "xAmmo": AmmoStart[0],
                        "yAmmo": AmmoStart[1] - 70})
                    sound("laser1.wav")
                    # Tắt hướng dẫn
                    guide = False

            # Bắn đạn của người chơi bằng phím cách
            elif event.type == KEYDOWN:
                if event.key == K_SPACE:
                    if (Game_Over == False):
                        #Thiết lập vị trí đạn ở trung tâm máy bay
                        AmmoStart = GetCenter(plane_image, plane_pos)
                        list_Ammo.append({
                            "xAmmo": AmmoStart[0],
                            "yAmmo": AmmoStart[1] - 70})
                        sound("laser1.wav")
                        #print(list_Ammo)
                        # Tắt hướng dẫn
                        guide = False
                
                # Thiết lập lại các giá trị khi bắt đầu lượt chơi mới sau Game Over
                if (event.key == K_r) and Game_Over: 
                        #Thiết lập các đối tượng
                        background = Background()
                        character = Character()
                        ### Thiết lập các giá trị ban đầu
                        #Vị trị ban đầu của 2 ảnh nền chuyển động
                        POS1, POS2 = [0, 0], [0, -955]
                        #Vị trí ban đầu của máy bay người chơi
                        plane_pos = [400, 800]
                        #Sô lượng máy bay địch tối đa
                        Number_Enemy = 1
                        #Số lượng đạn tối đa của địch
                        Number_EnemyAmmo = 1
                        #Tốc độ đạn của địch
                        EnemyAmmo_speed = 15
                        #Tốc đọ của địch
                        Enemy_Speed = 5
                        #Thiết lập điểm cao
                        if Score > High_Score:
                            High_Score = Score
                        #Thiết lập điểm thường
                        Score = 0
                        #Trạng thái Game Over
                        Game_Over = False
                        Game_Over_Music = True
                        mixer.music.play(-1)
                        guide = True

                        

        ### Khi chưa Game Over          
        if not Game_Over:
            # Danh sách tọa độ máy bay địch
            # Hạn chế số lượng địch
            if len(list_Enemy) < Number_Enemy:
                # Thiết lập vị trí ngẫu nhiên trong phạm vi quy định và cho vào danh sách
                list_Enemy.append({
                    "xEnemy": random.choice([1, 799]), 
                    "yEnemy": randint(1, 200)
                    })
            #Lấy tọa độ địch trong danh sách
            for i in list_Enemy:
                x, y = i["xEnemy"], i["yEnemy"]
                enemy_pos = [x, y]

            # Danh sách tọa độ đạn của địch 
            # Thiết lập vị trí đạn ở trung tâm máy bay
            Enemy_AmmoStart = GetCenter(enemy_image,enemy_pos)
            if len(list_EnemyAmmo) < Number_EnemyAmmo: 
                # Thiết lập vị trí và cho vào danh sách     
                list_EnemyAmmo.append({
                    "xEnemyAmmo" : Enemy_AmmoStart[0], 
                    "yEnemyAmmo" : Enemy_AmmoStart[1]})

            #Xử lý va chạm giữa đạn của người chơi và máy bay địch
            # Lấy tọa độ máy bay địch
            for countEnemy, iEnemy in enumerate(list_Enemy):
                xEnemy = iEnemy["xEnemy"]
                yEnemy = iEnemy["yEnemy"]
                #print(xEnemy, yEnemy)

                # Lấy tọa độ đạn của người chơi
                for countAmmo, iAmmo in enumerate(list_Ammo):
                    xAmmo = iAmmo["xAmmo"]
                    yAmmo = iAmmo["yAmmo"]
                    #print(xAmmo, yAmmo)

                    # Nếu xảy ra va chạm
                    if collision(enemy_image, (xEnemy, yEnemy), plane_ammo, (xAmmo, yAmmo)) == True:
                        # Xóa máy bay địch và đạn của người chơi
                        list_Enemy.remove(list_Enemy[countEnemy])
                        list_Ammo.remove(list_Ammo[countAmmo])
                        # Cộng điểm
                        Score += 100
                        # Điều chỉnh tốc độ máy bay địch và đạn của địch dựa vào số điểm hiện có
                        if Score in range(200, 10000, 200):
                            EnemyAmmo_speed += 5
                        if Score in range(300, 10000, 300):
                            Enemy_Speed += 2
        
            ### Xử lý Game Over
            # Điều kiện Game Over
            # Lấy tọa độ đạn của địch trong danh sách 
            for iEnemyAmmo in list_EnemyAmmo:
                xEnemyAmmo = iEnemyAmmo["xEnemyAmmo"]
                yEnemyAmmo = iEnemyAmmo["yEnemyAmmo"]
                #print(xEnemyAmmo, yEnemyAmmo)
                # Game Over khi xảy ra va chạm giữa đạn của địch và máy bay người chơi
                if collision(enemy_ammo, (xEnemyAmmo, yEnemyAmmo), plane_image, plane_pos) == True:
                    Game_Over = True
            #print(plane_pos)
            #print(list_EnemyAmmo)

            ## Vẽ các đối tượng
            # Vẽ chuyển động nền
            background.draw(POS1, POS2)
            # Vẽ máy bay người chơi
            character.draw_Plane(plane_pos)
            # Vẽ đạn của máy bay người chơi
            character.draw_Plane_Ammo(enemy_pos)
            # Vẽ máy bay địch
            character.draw_Enemy(plane_pos, Enemy_Speed)
            # Vẽ đạn của máy bay địch
            character.draw_Enemy_Ammo(plane_pos, EnemyAmmo_speed)
            # Hiển thị điểm số
            show_message(650, 20, "Score: {}".format(Score), 35)
            # Hiển thị điểm cao
            show_message(582, 50, " High Score: {}".format(High_Score), 35)
            # Hiển thị hướng dẫn đầu trò chơi
            if (guide):
                show_message(200, 300, "Move mouse to move your plane", 40)
                show_message(150, 350, "Hold Space key or press left mouse to shot", 40)
        
        # Khi Game Over
        if Game_Over:
            # Phát nhạc
            if Game_Over_Music:
                sound("gameover.wav")
            # Hiển thị thông báo Game Over và hướng dẫn để bắt đầu lượt chơi mới
            show_message(300, 450, "GAME OVER", 60)
            show_message(250, 550, "Press R to Replay", 60)
            # Ngừng phát nhạc nền
            Game_Over_Music = False
            mixer.music.stop()
        

        # Cập nhật màn hình
        pygame.display.update()
        # Thiết lập FPS cụ thể
        FPSCLOCK.tick(60)

# Khởi chạy hàm chính của game
if __name__  == "__main__":
    main()
