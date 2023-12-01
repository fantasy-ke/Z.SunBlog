<template>
	<div class="blog-article-container layout-padding main-box">
		<TreeFilter ref="categoryTreeRef" :request-api="getTreeTableList" id="value" :default-value="initParam.categoryId" @change="onChangeTree" />
		<div class="table-box">
			<ProTable
				ref="tableRef"
				:request-api="getTableList"
				:data-callback="dataCallBack"
				:columns="columns"
				:init-param="initParam"
				:tool-button="false"
			>
				<!-- v-auth="'article:add'" -->
				<template #tools>
					<el-button
						type="primary"
						icon="ele-Plus"
						@click="
							() => {
								router.push('/blog/article/operate');
							}
						"
					>
						新增
					</el-button></template
				>
				<template #cover="{ row }">
					<el-image shape="square" :size="100" fit="cover" :src="row.cover" />
				</template>
				<template #isTop="{ row }">
					<el-tag :type="row.isTop ? 'danger' : 'info'"> {{ row.isTop ? '是' : '否' }}</el-tag>
				</template>
				<template #status="{ row }">
					<el-tag :type="row.status === 0 ? 'success' : 'danger'"> {{ row.status === 0 ? '启用' : '禁用' }}</el-tag>
				</template>
				<template #creationType="{ row }">
					<el-tag :type="row.creationType === 0 ? 'success' : 'danger'"> {{ row.creationType === 0 ? '原创' : '转载' }}</el-tag>
				</template>
				<!-- v-auth="'article:edit'" -->
				<template #action="{ row }">
					<el-button
						icon="ele-Edit"
						size="small"
						text
						type="primary"
						@click="
							() => {
								router.push({ path: '/blog/article/operate', query: { tagsViewName: '编辑文章', id: row.id } });
							}
						"
					>
						编辑
					</el-button>
					<el-popconfirm title="确认删除吗？" @confirm="onDelete(row.id)">
						<template #reference>
							<!-- v-auth="'sysrole:delete'" -->
							<el-button icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
						</template>
					</el-popconfirm>
				</template>
			</ProTable>
		</div>
	</div>
</template>

<script setup lang="ts" name="blogArticle">
import { ref, reactive, inject } from 'vue';
import ProTable from '@/components/ProTable/index.vue';
import TreeFilter from '@/components/TreeFilter/index.vue';
import type { ColumnProps } from '@/components/ProTable/interface';
// import { auths } from '@/utils/authFunction';
import { useRouter } from 'vue-router';
import { ElMessage } from 'element-plus';
import { ArticlePageQueryInput, ArticleSsServiceProxy, CategorySsServiceProxy, KeyDto } from '@/shared/service-proxies';
import moment from 'moment';

const router = useRouter();
// 表格实例
const tableRef = ref<InstanceType<typeof ProTable>>();
const categoryTreeRef = ref<InstanceType<typeof TreeFilter>>();
const _articleService = new ArticleSsServiceProxy(inject('$baseurl'), inject('$api'));
const _categoryService = new CategorySsServiceProxy(inject('$baseurl'), inject('$api'));

const initParam = reactive<{ categoryId?: undefined | string }>({ categoryId: undefined });
const columns = reactive<ColumnProps[]>([
	{ type: 'index', label: '序号', width: 60 },
	{
		prop: 'title',
		label: '标题',
		search: {
			el: 'input',
		},
	},
	{
		prop: 'cover',
		label: '封面',
		width: 180,
	},
	{ prop: 'views', label: '浏览量', width: 100 },
	{ prop: 'isTop', label: '置顶', width: 80 },
	{ prop: 'creationType', label: '创作类型', width: 100 },
	{ prop: 'status', label: '状态', width: 100 },
	{
		prop: 'publishTime',
		label: '创建时间',
		width: 180,
	},
	{
		prop: 'action',
		label: '操作',
		align: 'center',
		fixed: 'right',
		width: 150,
		// isShow: auths(['article:edit', 'article:delete']),
	},
]);

const dataCallBack = (data) => {
	data.rows.forEach((res) => {
		if (res.publishTime) {
			res.publishTime = moment(res.publishTime).format('YYYY-MM-DD');
		}
	});
	return data;
};

const getTableList = (params: any) => {
	let newParams = JSON.parse(JSON.stringify(params)) as ArticlePageQueryInput;
	return _articleService.getPage(newParams);
};

const getTreeTableList = () => {
	return _categoryService.treeSelect();
};

const onChangeTree = (val?: undefined | string) => {
	initParam.categoryId = val;
};

// 删除
const onDelete = async (id: string) => {
	const { success } = await _articleService.delete({ id: id } as KeyDto);
	if (success) {
		tableRef.value?.reset();
		ElMessage.success('删除成功');
	}
};
</script>

<style scoped></style>
