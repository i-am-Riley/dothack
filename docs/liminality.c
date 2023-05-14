
//https://www.youtube.com/watch?v=f_9BwcJuqvI - 37:00

#include "sdvd.h"
#include "stream.h"
#include "data.h"


#define FUKU_DEBUG_OUT


//static ccFileList2 *f1;



//static int fileNum;



void ccInitFileList(void);
void ccAddFileList(ccFileList *);
void ccAddFileListOne(ccFileList *);
void ccAddFileListName(int, char *);
void ccsAllDeleteExceptCMN(void);
void ccsAllDeleteExceptGCMN(void);
void ccFileListDelete(ccFileList *);
int ccFileListDeleteOne(ccFileList *);
void ccsAllDelete(void);
void ccLoadResourceFL(void);
void ccLoadFLAdd(ccFileList *);
void ccLoadFLAddOne(ccFileList *);
static void ccLoadFLStart(void);


void ccAddRequestFileListTEST(void);
static void ccAddFileListName2(int,char*);
FILEDATA* searchFname(FILELIST*);
static int fileConflictCheck(ccFileList* );
//static void fileEsistCheck(int);
void ccFileEsistCheck(int);
static void ccFileListLoad(void*);
/*--- debug & test ---*/
static int     addFileCheck;
/*--- FILEDATA TABLE ---*/
extern FILEDATA pcCCSTbl[];
extern FILEDATA spcCCSTbl[];
extern FILEDATA npcCCSTbl[];
extern FILEDATA townCCSTbl[];
extern FILEDATA gimmickCCSTbl[];
extern FILEDATA enemyCCSTbl[];
extern FILEDATA desktopCCSTbl[];
extern FILEDATA toppageCCSTbl[];
extern FILEDATA fieldCCSTbl[];
extern FILEDATA menuCCSTbl[];
extern FILEDATA cmnCCSTbl[];
extern FILEDATA gcmnCCSTbl[];
extern FILEDATA equipCCSTbl[];
extern FILEDATA effectCCSTbl[];
extern FILEDATA dungeonCCSTbl[];
extern FILEDATA eventCCSTbl[];
extern FILEDATA bossCCSTbl[];
extern FILEDATA skillCCSTbl[];
/*-----------------------*/
static const int FILELIST_DIRECT_MAX = 16;
static FILEDATA directCCSTbl[FILELIST_DIRECT_MAX];
static int directNum;
/*---------------------------------------*/
static char *categoryPathTbl[]=[
       [DATA_FILE_DIR"cmn.bin"],
	   [DATA_FILE_DIR"gcmn.bin"],
	   [DATA_FILE_DIR"demo.bin"],
	   [DATA_FILE_DIR"desktop.bin"],
	   [DATA_FILE_DIR"toppage.bin"],
	   [DATA_FILE_DIR"menu.bin"],
	   [DATA_FILE_DIR"effect.bin"],
	   [DATA_FILE_DIR"equip.bin"],
	   [DATA_FILE_DIR"skill.bin"],
	   [DATA_FILE_DIR"spc.bin"],
	   [DATA_FILE_DIR"pc.bin"],
	   [DATA_FILE_DIR"npc.bin"],
	   [DATA_FILE_DIR"gimmic.bin"],
	   [DATA_FILE_DIR"enemy.bin"],
	   [DATA_FILE_DIR"boss.bin"],
	   [DATA_FILE_DIR"town.bin"],
	   [DATA_FILE_DIR"field.bin"],
	   [DATA_FILE_DIR"dungeon.bin"],
	   [DATA_FILE_DIR"event.bin"],
	   [DATA_FILE_DIR],
];
static FILEDATA *categoryFDTbl[]=[
       &cmnCCSTbl[0],
	   &gcmnCCSTbl[0],
	   NULL,
	   &desktopCCSTbl[0],
	   &toppageCCSTbl[0],
	   &menuCCSTbl[0],
	   &effectCCSTbl[0],
	   &equipCCSTbl[0],
	   &skillCCSTbl[0],
	   &spcCCSTbl[0],
	   &pcCCSTbl[0],
	   &npcCCSTbl[0],
	   &gimmickCCSTbl[0],
	   &enemyCCSTbl[0],
	   &bossCCSTbl[0],
	   &townCCSTbl[0],
	   &fieldCCSTbl[0],
	   &dungeonCCSTbl[0],
	   &eventCCSTbl[0],
	   &directCCSTbl[0],
];
static u_long   cateCDOfsTbl[]=[
       _CMN_OFS,
	   _GCMN_OFS,
	   NULL,
	   _DESKTOP_OFS,
	   _TOPPAGE_OFS,
	   _MENU_OFS,
	   _EFFECT_OFS,
	   _EQUIP_OFS,
	   _SKILL_OFS,
	   _SPC_OFS,
	   _PC_OFS,
	   _NPC_OFS,
	   _GIMMICK_OFS,
	   _ENEMY_OFS,
	   _BOSS_OFS,
	   _TOWN_OFS,
	   _FIELD_OFS,
	   _DUNGEON_OFS,
	   _EVENT_OFS,
	   NULL
];
//------------------------------------------------//
//                                                //
//------------------------------------------------//
static const int SCENE_FILELIST_MAX = 64 + 32;